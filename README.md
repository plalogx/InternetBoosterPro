# InternetBooster Pro (C# / .NET 8 / WPF)


## Быстрый старт 
### Шаг 1. Установите .NET 8 SDK

1. Откройте страницу: https://dotnet.microsoft.com/download/dotnet/8.0
2. Скачайте **".NET 8.0 SDK"** (именно **SDK**, не "Runtime" — Runtime для сборки не подходит), колонку **Windows x64**.
3. Установите как обычную программу (Далее → Далее → Установить).
4. Перезапустите компьютер (иногда обязательно, чтобы обновился PATH).

Проверка, что всё встало: откройте обычную командную строку (`cmd`,
не PowerShell) и наберите:
```
dotnet --version
```
Должно вывести что-то вроде `8.0.xxx`. Если пишет "команда не найдена" — SDK не установился или нужен перезапуск ПК.

### Шаг 2. Распакуйте архив проекта

Распакуйте `InternetBoosterPro_CSharp.zip` в удобное место, например на Рабочий стол.
После распаковки у вас должна получиться такая структура:

```
Desktop\
  InternetBoosterPro_CSharp\
    InternetBoosterPro\              <-- ВОТ ЭТА ПАПКА НАМ НУЖНА
      InternetBoosterPro.csproj      <-- главный файл проекта
      build.bat                      <-- собрать проект (двойной клик)
      run.bat                        <-- собрать (если нужно) и запустить
      App.xaml
      App.xaml.cs
      Core\
      UI\
      Tray\
      assets\
      README.md (этот файл)
```

### Шаг 3. Запустите приложение через run.bat

После успешной сборки дважды кликните по файлу **`run.bat`** — он
найдёт `InternetBoosterPro.exe` и сразу его откроет.
Готовый .exe при этом лежит здесь:
```
InternetBoosterPro\bin\Release\net8.0-windows\InternetBoosterPro.exe
```

Можно также запускать его напрямую двойным щелчком — это уже рабочее
приложение. **Важно:** рядом с .exe в этой же папке лежит куча
`.dll`-файлов — их нельзя удалять и нельзя переносить один .exe без
них, иначе приложение не запустится

### `dotnet: команда не найдена` / `не является внутренней или внешней командой`
.NET SDK не установлен или не подхватился в PATH. Переустановите SDK
(Шаг 1) и перезапустите компьютер.


---

## Структура проекта (соответствие исходным .py модулям)

| C# | Python |
|---|---|
| `Core/Config.cs` | `core/config.py` |
| `Core/Logger.cs` | `core/logger.py` |
| `Core/Admin.cs` | `core/admin.py` |
| `Core/Adapter.cs` | `core/adapter.py` |
| `Core/Background.cs` | `core/background.py` |
| `Core/Backup.cs` | `core/backup.py` |
| `Core/CoreGaming.cs` | `core/core_gaming.py` |
| `Core/DnsManager.cs` | `core/dns.py` |
| `Core/NetworkMonitor.cs` | `core/network.py` |
| `Core/Notifier.cs` | `core/notifier.py` |
| `Core/Optimizer.cs` | `core/optimizer.py` |
| `Core/Scheduler.cs` | `core/scheduler.py` |
| `Core/Startup.cs` | `core/startup.py` |
| `Core/TcpTools.cs` | `core/tcp.py` |
| `Core/WarpManager.cs` | `core/warp_manager.py` |
| `Core/AesCtr.cs` | (часть `cryptography` из mtproto_proxy.py / mtproto_selftest.py) |
| `Core/MtprotoSecret.cs` | `core/mtproto_secret.py` |
| `Core/MtprotoProxy.cs` | `core/mtproto_proxy.py` |
| `Core/MtprotoSelftest.cs` | `core/mtproto_selftest.py` |
| `Core/Socks5Proxy.cs` | `core/socks5_proxy.py` |
| `Tray/TrayIconManager.cs` | `tray/tray_tray_icon.py` |
| `UI/MainWindow.xaml(.cs)` | `ui/main_window.py` |
| `UI/DashboardControl.xaml(.cs)` | `ui/dashboard.py` |
| `UI/SettingsControl.xaml(.cs)` | `ui/settings.py` |
| `UI/LogsControl.xaml(.cs)` | `ui/logs.py` |
| `UI/SecretWidgetControl.xaml(.cs)` | `ui/secret_widget.py` |
| `UI/WarpWidgetControl.xaml(.cs)` | `ui/warp_widget.py` |
| `UI/MtprotoWidgetControl.xaml(.cs)` | `ui/mtproto_widget.py` |
| `UI/Socks5WidgetControl.xaml(.cs)` | `ui/socks5_widget.py` |
| `App.xaml(.cs)` | `main.py` |

## Безопасность / права администратора

Приложение запускается как обычный процесс (`asInvoker`, см.
`app.manifest`) и само проверяет права (`Core.Admin.IsAdmin()`),
предлагая перезапуск от администратора там, где это нужно (открытие
портов в брандмауэре, изменение DNS/TCP, автозапуск) — то есть ведёт
себя так же, как и Python-версия.
