VAR currentStoryKnot = "greet"

I've known a few humans in my time.
I've liked all of them for different reasons.
~ currentStoryKnot = "about1"
-> END

=== about1 ===
I miss my home. 
I feel lighter and brighter when I'm away for a little bit, but in the end, I belong among my people.
~ currentStoryKnot = "about2"
-> END

=== about2 ===
Aren't you just a pleasant surprise! It warms me up to see someone doing their best!

 * [Who are you?]
    None of us are anybody in particular, I suppose. Unlike you!
        * * [Who is "us"?]
            Spirits! We are spirits. Each slightly different, but all the same.
            We do age, and I've aged a great deal.
        * * [What do you mean by, "Unlike you?"]
            Humans! Is that rude to say? You're all marvelously unique.
 * [Where do you come from?]
    I'll have to think on that... I forget things so easily nowadays!

- Keep going! You're building a beautiful home.
~ currentStoryKnot = "THE_END"
-> END

=== THE_END ===
-> END