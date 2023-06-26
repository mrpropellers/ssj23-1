namespace LeftOut.GameJam.Bonsai
{
    /// <summary>
    /// Simple data container for keeping track of how many branches we've sprouted in a given grow cycle
    /// </summary>
    class BranchSproutingContext
    {
        internal BranchSproutingContext(int sproutCount)
        {
            SproutsRemaining = sproutCount;
        }
        
        // The number of sprouts allowed to be added this cycle
        internal int SproutsRemaining { get; private set; }

        // Effectively just the setter for our SproutsRemaining value
        internal void AcknowledgeNewSprout()
        {
            SproutsRemaining--;
        }
    }
}
