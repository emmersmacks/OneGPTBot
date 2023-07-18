﻿using ChatBotGPT;
using ChatBotGPT.ChatGPT;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;
using VideoBot;
using VideoBot.Services;
using Message = ChatBotGPT.ChatGPT.Message;

namespace BotTest;

public class GptResponseTest
{
    private ServiceProvider _serviceProvider;
    
    [SetUp]
    public void Setup()
    {
        var setup = new Setup();
        _serviceProvider = setup.Init();
    }
    
    [Test]
    public async Task TestAccessDataService()
    {
        var messageService = _serviceProvider.GetService(typeof(TelegramMessagesService)) as TelegramMessagesService;
        var gptClient = _serviceProvider.GetService(typeof(GPTClient)) as GPTClient;

        var messages = new List<string>()
        {
            "user:Напиши очень длинную сказку про летающих алкоголиков в вертолетах",
            "assistant:Жили-были необычные друзья - Люк и Джонни. Они были любителями путешествий, обожали экстрим и всегда готовы были к новым приключениям.\n\nОднажды, Люк и Джонни отправились в путешествие по стране, где щедро употребляли различные виды алкогольных напитков. Они испробовали все, начиная от пива и заканчивая самогоном.\n\nОднако, когда они обнаружили старый, но все еще рабочий вертолет у одного из местных жителей, у них появилась новая, увлекательная идея - летать на нем и путешествовать в воздухе, объезжая горы, леса и озера.\n\nОни решили, что путешествия в вертолете будут идеальным способом наслаждаться быстрым пейзажами и красотой природы, особенно, когда они наполовину выпивши, <летел> с такой скользкостью, что вертолет заводился сам.\n\nИх приключения начались с головокружительных полетов над грандиозными каньонами и мощными водопадами. Они увлекались адреналином и уверенностью в своих способностях пилота.\n\nОднако, как это не бывает при однообразных действиях, им стало постепенно надоедать ограниченность в путешествиях. Что же, они придумали новый и неповторимый план - летать ночью по городам и городкам, чтобы узнать все их тайны и секреты.\n\nНочные приключения были невероятными: они испытывали свою удачу, забредая в различные кварталы и районы, исследуя скрытые сокровищные причуды малых наследий.\n\nДля них было важно остаться под прицелом, не раскрыться местным жителям, поэтому они никогда не посещали одни и те же места, делали рискованные маневры в воздухе, чтобы сохранить инкогнито.\n\nОдин из самых запоминающихся маршрутов они прошли над большим городом, где были свидетелями странных происшествий. Они заметили небольшие светящиеся шары, которые медленно зависали в воздухе и затем исчезали.\n\nНо это была не их проблема, поэтому они просто посмотрели и продолжили свой путь, вечер был темным, и они решили вернуться.\n\nНаконец, пришло время их новых приключений, и они отправились на путешествие по всей стране, летая на своем уникальном вертолете.\n\nИх ждут еще множество удивительных мест и ощущений, и они знают, что каждый новый полет только укрепляет их дух и уверенность в себе. Но самое главное, что они поня",
            "user:Напиши очень длинную сказку про летающих алкоголиков в вертолетах",
            "assistant:Жили-были необычные друзья - Люк и Джонни. Они были любителями путешествий, обожали экстрим и всегда готовы были к новым приключениям.\n\nОднажды, Люк и Джонни отправились в путешествие по стране, где щедро употребляли различные виды алкогольных напитков. Они испробовали все, начиная от пива и заканчивая самогоном.\n\nОднако, когда они обнаружили старый, но все еще рабочий вертолет у одного из местных жителей, у них появилась новая, увлекательная идея - летать на нем и путешествовать в воздухе, объезжая горы, леса и озера.\n\nОни решили, что путешествия в вертолете будут идеальным способом наслаждаться быстрым пейзажами и красотой природы, особенно, когда они наполовину выпивши, <летел> с такой скользкостью, что вертолет заводился сам.\n\nИх приключения начались с головокружительных полетов над грандиозными каньонами и мощными водопадами. Они увлекались адреналином и уверенностью в своих способностях пилота.\n\nОднако, как это не бывает при однообразных действиях, им стало постепенно надоедать ограниченность в путешествиях. Что же, они придумали новый и неповторимый план - летать ночью по городам и городкам, чтобы узнать все их тайны и секреты.\n\nНочные приключения были невероятными: они испытывали свою удачу, забредая в различные кварталы и районы, исследуя скрытые сокровищные причуды малых наследий.\n\nДля них было важно остаться под прицелом, не раскрыться местным жителям, поэтому они никогда не посещали одни и те же места, делали рискованные маневры в воздухе, чтобы сохранить инкогнито.\n\nОдин из самых запоминающихся маршрутов они прошли над большим городом, где были свидетелями странных происшествий. Они заметили небольшие светящиеся шары, которые медленно зависали в воздухе и затем исчезали.\n\nНо это была не их проблема, поэтому они просто посмотрели и продолжили свой путь, вечер был темным, и они решили вернуться.\n\nНаконец, пришло время их новых приключений, и они отправились на путешествие по всей стране, летая на своем уникальном вертолете.\n\nИх ждут еще множество удивительных мест и ощущений, и они знают, что каждый новый полет только укрепляет их дух и уверенность в себе. Но самое главное, что они поня",
            "user:Напиши очень длинную сказку про летающих алкоголиков в вертолетах",
            "assistant:Жили-были необычные друзья - Люк и Джонни. Они были любителями путешествий, обожали экстрим и всегда готовы были к новым приключениям.\n\nОднажды, Люк и Джонни отправились в путешествие по стране, где щедро употребляли различные виды алкогольных напитков. Они испробовали все, начиная от пива и заканчивая самогоном.\n\nОднако, когда они обнаружили старый, но все еще рабочий вертолет у одного из местных жителей, у них появилась новая, увлекательная идея - летать на нем и путешествовать в воздухе, объезжая горы, леса и озера.\n\nОни решили, что путешествия в вертолете будут идеальным способом наслаждаться быстрым пейзажами и красотой природы, особенно, когда они наполовину выпивши, <летел> с такой скользкостью, что вертолет заводился сам.\n\nИх приключения начались с головокружительных полетов над грандиозными каньонами и мощными водопадами. Они увлекались адреналином и уверенностью в своих способностях пилота.\n\nОднако, как это не бывает при однообразных действиях, им стало постепенно надоедать ограниченность в путешествиях. Что же, они придумали новый и неповторимый план - летать ночью по городам и городкам, чтобы узнать все их тайны и секреты.\n\nНочные приключения были невероятными: они испытывали свою удачу, забредая в различные кварталы и районы, исследуя скрытые сокровищные причуды малых наследий.\n\nДля них было важно остаться под прицелом, не раскрыться местным жителям, поэтому они никогда не посещали одни и те же места, делали рискованные маневры в воздухе, чтобы сохранить инкогнито.\n\nОдин из самых запоминающихся маршрутов они прошли над большим городом, где были свидетелями странных происшествий. Они заметили небольшие светящиеся шары, которые медленно зависали в воздухе и затем исчезали.\n\nНо это была не их проблема, поэтому они просто посмотрели и продолжили свой путь, вечер был темным, и они решили вернуться.\n\nНаконец, пришло время их новых приключений, и они отправились на путешествие по всей стране, летая на своем уникальном вертолете.\n\nИх ждут еще множество удивительных мест и ощущений, и они знают, что каждый новый полет только укрепляет их дух и уверенность в себе. Но самое главное, что они поня",
            "user:Напиши очень длинную сказку про летающих алкоголиков в вертолетах",
            "assistant:Жили-были необычные друзья - Люк и Джонни. Они были любителями путешествий, обожали экстрим и всегда готовы были к новым приключениям.\n\nОднажды, Люк и Джонни отправились в путешествие по стране, где щедро употребляли различные виды алкогольных напитков. Они испробовали все, начиная от пива и заканчивая самогоном.\n\nОднако, когда они обнаружили старый, но все еще рабочий вертолет у одного из местных жителей, у них появилась новая, увлекательная идея - летать на нем и путешествовать в воздухе, объезжая горы, леса и озера.\n\nОни решили, что путешествия в вертолете будут идеальным способом наслаждаться быстрым пейзажами и красотой природы, особенно, когда они наполовину выпивши, <летел> с такой скользкостью, что вертолет заводился сам.\n\nИх приключения начались с головокружительных полетов над грандиозными каньонами и мощными водопадами. Они увлекались адреналином и уверенностью в своих способностях пилота.\n\nОднако, как это не бывает при однообразных действиях, им стало постепенно надоедать ограниченность в путешествиях. Что же, они придумали новый и неповторимый план - летать ночью по городам и городкам, чтобы узнать все их тайны и секреты.\n\nНочные приключения были невероятными: они испытывали свою удачу, забредая в различные кварталы и районы, исследуя скрытые сокровищные причуды малых наследий.\n\nДля них было важно остаться под прицелом, не раскрыться местным жителям, поэтому они никогда не посещали одни и те же места, делали рискованные маневры в воздухе, чтобы сохранить инкогнито.\n\nОдин из самых запоминающихся маршрутов они прошли над большим городом, где были свидетелями странных происшествий. Они заметили небольшие светящиеся шары, которые медленно зависали в воздухе и затем исчезали.\n\nНо это была не их проблема, поэтому они просто посмотрели и продолжили свой путь, вечер был темным, и они решили вернуться.\n\nНаконец, пришло время их новых приключений, и они отправились на путешествие по всей стране, летая на своем уникальном вертолете.\n\nИх ждут еще множество удивительных мест и ощущений, и они знают, что каждый новый полет только укрепляет их дух и уверенность в себе. Но самое главное, что они поня",
        };

        var responseType = await gptClient.SendMessage("Hello!", messages);

        Assert.NotNull(responseType.Error.error);
        Assert.Null(responseType.Message.choices);
    }
}