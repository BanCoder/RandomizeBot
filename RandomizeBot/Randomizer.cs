using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RandomizeBot
{
	internal class Randomizer
	{
		private static int RandomizeNumber()
		{
			Random random = new Random();
			int randomNumber = random.Next(0, 11);
			return randomNumber;
		}
		public static async Task CheckCorrectNumber(ITelegramBotClient botClient, Update update, CancellationToken ct, int userNumber)
		{
			int correctNumber = RandomizeNumber(); 
			if (userNumber == correctNumber)
			{
				await botClient.SendTextMessageAsync(
					chatId: update.Message.Chat.Id,
					text: "Поздравляю, вы угадали число!\nВот ваш приз!",
					cancellationToken: ct);
				var sticker = await botClient.SendStickerAsync(
					chatId: update.Message.Chat.Id, 
					sticker: InputFile.FromString("CAACAgIAAxkBAAERiVFopJIUEPurOOMFLLotjSo8AAFBm-EAArMLAAIqUFFKLKO2Cexp09Y2BA"), 
					cancellationToken: ct
					); 
			}
			else
			{
				await botClient.SendTextMessageAsync(
					chatId: update.Message.Chat.Id,
					text: $"Неверно! Загаданное число {correctNumber}",
					cancellationToken: ct);
				var sticker = await botClient.SendStickerAsync(
					chatId: update.Message.Chat.Id,
					sticker: InputFile.FromString("CAACAgIAAxkBAAERjzxopb_gjv_ensnX2fhQPuTGOH0r4AAC1gADVp29Cgl1yQziLyEKNgQ"),
					cancellationToken: ct
					);
			}
		}
	}
}
