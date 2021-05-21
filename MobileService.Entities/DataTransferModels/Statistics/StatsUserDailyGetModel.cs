namespace MobileService.Entities.DataTransferModels.Statistics
{
    /// <summary>
    /// <list type="bullet|number|table">
    ///     <item>
    ///         <term>TotalFlashcards</term>
    ///         <description>Count of all flashcards user has 2 x total added words.</description>
    ///     </item>
    ///     <item>
    ///         <term>NewFlashcards</term>
    ///         <description>Count of all flashcards user already added and never turn out.</description>
    ///     </item>
    ///     <item>
    ///         <term>ToLearnFlashcards</term>
    ///         <description>Count of all flashcards which are expired for today.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public class StatsUserDailyGetModel
    {
        public int TotalFlashcards { get; set; }
        public int NewFlashcards { get; set; }
        public int ToLearnFlashcards { get; set; }
    }
}
