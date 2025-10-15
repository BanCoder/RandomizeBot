using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RandomizeBot
{
	internal class RandomHandler
	{
		private static string _token = App.Settings.Token;
		private static TelegramBotClient _client;
		private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
		{
			if (update.Message?.Text is { } messageText)
			{
				Console.WriteLine($"Получено сообщение: {messageText}");
				switch (messageText)
				{
					case "/start":
						await botClient.SendTextMessageAsync(
							chatId: update.Message.Chat.Id,
							text: "Введите число от 0 до 10:",
							cancellationToken: cancellationToken);
						break;
					default:
						if (int.TryParse(messageText, out int number) && number <= 10)
						{
							await Randomizer.CheckCorrectNumber(botClient, update, cancellationToken, number);
							break;
						}
						else
						{
							await botClient.SendTextMessageAsync(
								chatId: update.Message.Chat.Id,
								text: "Неверная команда! Нужно ввести число от 0 до 10!",
								cancellationToken: cancellationToken);
						}
						break;
				}
			}
		}
		private static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
		{
			Console.WriteLine($"Ошибка: {exception.Message}");
			return Task.CompletedTask;
		}
		public void Starter()
		{
			_client = new TelegramBotClient(_token);
			var receiverOptions = new ReceiverOptions
			{
				AllowedUpdates = Array.Empty<UpdateType>()
			};
			_client.StartReceiving(
				updateHandler: HandleUpdateAsync,
				pollingErrorHandler: HandlePollingErrorAsync,
				receiverOptions: receiverOptions
			);

			Console.WriteLine("Бот запущен. Нажмите Enter для остановки...");
			Console.ReadLine();
		}
	}
}
