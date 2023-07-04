using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

internal class Program
{
    private static void Main(string[] args)
    {
        var token = "6310995756:AAFuWdJ045tzy2eDEKezmseUZkqw_VrREBM";
        var botClient = new TelegramBotClient(token);

        Console.WriteLine("Bot started !");
        CancellationToken cts = new();

        ReceiverOptions receiverOptions = new()
        {
            AllowedUpdates = { }
        };

        botClient.StartReceiving(
            updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandlePollingErrorAsync,
            receiverOptions: receiverOptions,
            cancellationToken: cts
        );

        Console.ReadLine();
    }
    private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {

        Console.WriteLine($"Message : {update.Message.Text} from : {update.Message.Chat.Username} hello=>>>>>");

        if (update.Message is not { } message)
            return;
        if (message.Text is not { } messageText)
            return;

        var chatId = message.Chat.Id;
        var userFirstName = message.From.Username;

        if (messageText.ToLower().Contains("/start"))
        {
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"Hello  @{userFirstName}",
                cancellationToken: cancellationToken);
            return;
        }

        if (messageText.ToLower().Contains("hello"))
        {
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"Hi  @{userFirstName}",
                cancellationToken: cancellationToken);
            return;
        }

        if (messageText.ToLower().Contains("привет"))
        {
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"Привет  @{userFirstName}",
                cancellationToken: cancellationToken);
            return;
        }

        if (messageText.ToLower().Contains("салом"))
        {
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"Салом  @{userFirstName}",
                cancellationToken: cancellationToken);
            return;
        }
        if (messageText.ToLower().Contains("sticker"))
        {
            List<string> stickerUrls = new List<string>
        {
            "😀",
            "😎",
            "🤣",
            "🤐",
            "😴",
            "🥶",
            "😯",
            "😡"
        };

            Random random = new Random();
            int randomIndex = random.Next(0, stickerUrls.Count);

            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"{stickerUrls[randomIndex]}",
                cancellationToken: cancellationToken);
            return;
        }

        if (messageText.ToLower().Contains("video"))
        {
            List<string> videoUrls = new List<string>
        {
            "https://github.com/TelegramBots/book/raw/master/src/docs/video-bulb.mp4"
        };

            Random random = new Random();
            int randomIndex = random.Next(0, videoUrls.Count);
            await botClient.SendVideoAsync(
                chatId: chatId,
                video: $"{videoUrls[randomIndex]}",
                cancellationToken: cancellationToken);
            return;
        }

        if (messageText.ToLower().Contains("crm.softclub.tj"))
        {
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Шумо метавонед бо ин url давомоти худатонро бинед",
                parseMode: ParseMode.MarkdownV2,
                disableNotification: true,
                replyToMessageId: update.Message.MessageId,
                replyMarkup: new InlineKeyboardMarkup(
                    InlineKeyboardButton.WithUrl(
                        text: "SoftClub.tj",
                        url: "https://crm.softclub.tj/")),
                cancellationToken: cancellationToken);
            return;
        }

        if (messageText.ToLower().Contains("photo"))
        {
            List<string> photoUrls = new List<string>
        {
            "https://github.com/TelegramBots/book/raw/master/src/docs/photo-ara.jpg",
            "https://static.wikia.nocookie.net/wikies/images/4/43/Logo-csharp.png/revision/latest?cb=20180617092325&path-prefix=ru",
            "https://www.freecodecamp.org/news/content/images/size/w2000/2023/06/csharp.png"
        };
            Random random = new Random();
            int randomIndex = random.Next(0, photoUrls.Count);
            await botClient.SendPhotoAsync(
                chatId: chatId,
                photo: $"{photoUrls[randomIndex]}",
                parseMode: ParseMode.Html,
                cancellationToken: cancellationToken);
            return;
        }

        if (messageText.ToLower().Contains("audio"))
        {
            List<string> audioUrls = new List<string>
        {
            "https://github.com/TelegramBots/book/raw/master/src/docs/audio-guitar.mp3"
        };

            Random random = new Random();
            int randomIndex = random.Next(0, audioUrls.Count);
            await botClient.SendAudioAsync(
                chatId: chatId,
                audio: $"{audioUrls[randomIndex]}",
                cancellationToken: cancellationToken);
            return;
        }

        if (messageText.ToLower().Contains("file"))
        {
            List<string> fileUrls = new List<string>
        {
            "https://github.com/TelegramBots/book/raw/master/src/docs/photo-ara.jpg"
            //"https.//Exam 24.06.2023.pdf"
        };
            Random random = new Random();
            int randomIndex = random.Next(0, fileUrls.Count);
            await botClient.SendDocumentAsync(
                chatId: chatId,
                document: $"{fileUrls[randomIndex]}",
                parseMode: ParseMode.Html,
                cancellationToken: cancellationToken);
            return;
        }

        var replyKeyboardRemove = new ReplyKeyboardRemove();
        var keyboard = new KeyboardButton[][]
        {
        new KeyboardButton[]
        {
            new KeyboardButton("crm.softclub.tj"),
            new KeyboardButton("file")
        },
        new KeyboardButton[]
        {
            new KeyboardButton("audio"),
            new KeyboardButton("sticker")
        }
        };
        var replyKeyboardMarkup = new ReplyKeyboardMarkup(keyboard)
        {
            ResizeKeyboard = true,
            OneTimeKeyboard = true // Optional: hide keyboard after selection
        };




        //await botClient.SendTextMessageAsync(
        //  chatId: chatId,
        //  text: "Choose an option:",
        //  replyMarkup: replyKeyboardMarkup,
        //  cancellationToken: cancellationToken);


        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "I'm sorry, I don't understand.",
            cancellationToken: cancellationToken);
    }

    private static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(errorMessage);
        return Task.CompletedTask;
    }
}
