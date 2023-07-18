using VideoBot.Handlers;
using VideoBot.Services;

namespace Telegram.Bot.Types.ReplyMarkups
{
    public class TelegramKeyboard
    {
        private readonly AccessDataService _accessDataService;

        public TelegramKeyboard(AccessDataService accessDataService)
        {
            _accessDataService = accessDataService;
        }

        public InlineKeyboardButton GetButton<T>(string label) where T : ICallbackHandler
        {
            var type = typeof(T);
            var button = InlineKeyboardButton.WithCallbackData(label, type.Name);
            return button;
        }
    }
}

