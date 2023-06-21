﻿namespace LeftOut.GameJam.Bonsai
{
    class BonsaiGrowerContext
    {
        internal BonsaiGrowerContext(int sproutCount)
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
