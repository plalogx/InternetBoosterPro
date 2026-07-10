using System.ComponentModel;
using System.Net;
using System.Windows;

namespace InternetBoosterPro;

public partial class MainWindow : Window
{
    private MtprotoProxy? _proxy;

    public MainWindow()
    {
        InitializeComponent();
        SecretTextBox.Text = MtprotoProxy.FormatHexSecret(MtprotoProxy.GenerateSecret());
        UpdateProxyLinkText();
    }

    private void Log(string message)
    {
        var line = $"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}";
        if (Dispatcher.HasShutdownStarted || Dispatcher.HasShutdownFinished)
        {
            return;
        }

        Dispatcher.Invoke(() =>
        {
            LogTextBox.AppendText(line);
            LogTextBox.ScrollToEnd();
        });
    }

    private void UpdateProxyLinkText()
    {
        if (MtprotoProxy.TryParseSecret(SecretTextBox.Text, out _))
        {
            ProxyLinkTextBlock.Text = $"Proxy link: tg://proxy?server=127.0.0.1&port={PortTextBox.Text.Trim()}&secret={SecretTextBox.Text.Trim()}";
            CopyLinkButton.IsEnabled = true;
        }
        else
        {
            ProxyLinkTextBlock.Text = "Enter or generate a 32-character hex secret to build the proxy link.";
            CopyLinkButton.IsEnabled = false;
        }
    }

    private void GenerateSecretButton_Click(object sender, RoutedEventArgs e)
    {
        SecretTextBox.Text = MtprotoProxy.FormatHexSecret(MtprotoProxy.GenerateSecret());
        UpdateProxyLinkText();
        Log("Generated new MTProto secret.");
    }

    private async void StartStopButton_Click(object sender, RoutedEventArgs e)
    {
        if (_proxy != null)
        {
            await StopProxyAsync();
            return;
        }

        if (!int.TryParse(PortTextBox.Text.Trim(), out var port) || port <= 0 || port > 65535)
        {
            Log("Invalid port. Enter a value between 1 and 65535.");
            return;
        }

        if (!MtprotoProxy.TryParseSecret(SecretTextBox.Text, out var secret))
        {
            Log("Invalid secret. The proxy secret should be 32 hex characters (16 bytes). Example: 5f2e1a... ");
            return;
        }

        _proxy = new MtprotoProxy(port, secret, Log);
        try
        {
            await _proxy.StartAsync();
            Log($"Proxy started on 127.0.0.1:{port}.");
            StartStopButton.Content = "Stop proxy";
            PortTextBox.IsEnabled = false;
            SecretTextBox.IsEnabled = false;
            GenerateSecretButton.IsEnabled = false;
            CopyLinkButton.IsEnabled = true;
            UpdateProxyLinkText();
        }
        catch (Exception ex)
        {
            Log($"Failed to start proxy: {ex.Message}");
            _proxy = null;
        }
    }

    private async Task StopProxyAsync()
    {
        if (_proxy == null)
        {
            return;
        }

        try
        {
            await _proxy.StopAsync();
            Log("Proxy stopped.");
        }
        catch (Exception ex)
        {
            Log($"Error stopping proxy: {ex.Message}");
        }
        finally
        {
            _proxy = null;
            StartStopButton.Content = "Start proxy";
            PortTextBox.IsEnabled = true;
            SecretTextBox.IsEnabled = true;
            GenerateSecretButton.IsEnabled = true;
            UpdateProxyLinkText();
        }
    }

    private void CopyLinkButton_Click(object sender, RoutedEventArgs e)
    {
        if (!MtprotoProxy.TryParseSecret(SecretTextBox.Text, out _))
        {
            Log("Cannot copy link because the secret is invalid.");
            return;
        }

        var link = $"tg://proxy?server=127.0.0.1&port={PortTextBox.Text.Trim()}&secret={SecretTextBox.Text.Trim()}";
        Clipboard.SetText(link);
        Log("Proxy link copied to clipboard.");
    }

    private void SecretTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        UpdateProxyLinkText();
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        if (_proxy != null)
        {
            _proxy.StopAsync().GetAwaiter().GetResult();
        }

        base.OnClosing(e);
    }
}
