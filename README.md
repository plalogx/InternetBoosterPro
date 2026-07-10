# Internet Booster Pro

Internet Booster Pro — это простой локальный MTProto-прокси для Telegram на Windows. Приложение позволяет запускать собственный прокси-сервер на `127.0.0.1`, генерировать секрет и быстро копировать ссылку `tg://proxy`, чтобы подключить его в Telegram.

## Возможности

- Локальный MTProto-прокси для Windows
- Генерация секретных ключей (16 байт, 32 hex-символа)
- Запуск и остановка прокси одним кликом
- Копирование готовой `tg://proxy` ссылки
- Поддержка Telegram DC (Data Center) через штатные адреса Telegram

## Требования

- Windows 10 или Windows 11
- .NET 8 SDK / Runtime

## Быстрый старт

1. Скачайте репозиторий.
2. Откройте проект `InternetBoosterPro/InternetBoosterPro.csproj` в Visual Studio или в командной строке выполните:

```powershell
cd InternetBoosterPro
dotnet run
```

3. В приложении задайте порт (по умолчанию `1443`) и нажмите «Generate secret», чтобы получить новый секрет.
4. Нажмите «Start proxy». Прокси запустится на `127.0.0.1`.
5. Нажмите «Copy tg://proxy link», чтобы скопировать ссылку и вставить её в Telegram.

## Сборка

```powershell
cd InternetBoosterPro
dotnet build --configuration Release
```

## Структура проекта

- `InternetBoosterPro/InternetBoosterPro.csproj` — основной проект WPF на .NET 8
- `InternetBoosterPro/MainWindow.xaml` — интерфейс приложения
- `InternetBoosterPro/Core/MtprotoProxy.cs` — реализация MTProto-прокси

## Лицензия

Проект распространяется под лицензией MIT. Смотрите файл `LICENSE`.

## Важно

Приложение предназначено для локального использования и учебных целей. Убедитесь, что вы используете его в соответствии с правилами Telegram и действующим законодательством.
