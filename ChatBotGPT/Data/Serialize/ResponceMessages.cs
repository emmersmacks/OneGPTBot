using VideoBot.Data;

namespace VideoBot;

[Serializable]
public class ResponceMessages
{
    [Description("Приветственное сообщение")]
    public string StartMessage ;
    public string[] Notifications;
    [Description("Ответное сообщение, если пользователь выбрал горизонтальные видео")]
    public string SelectHorizontal;
    [Description("Ответное сообщение, если пользователь выбрал вертикальные видео")]
    public string SelectVertical;
    [Description("Сообщение об изменении формата")]
    public string NewFormatMessage;
    [Description("Сообщение отправляемое пользователю после успешной публикации его видео")]
    public string VideoSendedMessage;
    [Description("Надпись на кнопке для горизонтальных видео")]
    public string HorizontalButtonText;
    [Description("Надпись на кнопке для вертикальных видео")]
    public string VerticalButtonText;
    [Description("Надпись на кнопке для отправки видео")]
    public string SendLinkButtonText;
    [Description("Сообщение после нажатия на кнопку для отправки видео")]
    public string SendLinkMessage;
    [Description("Сообщение, отправляемое в канал 'ОТЧЕТЫ'")]
    public string ChanelMessage;
}