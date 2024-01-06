INCLUDE Globals.ink 

Hey, do you know where the staff room is? #speaker:Takahashi Tanjiro #animationBool:isTalk
Staff room? You should have known about that by now, Takahashi #speaker:Nerd Student #animationBool:isTalk
The hell, he knows my name??? #speaker:Takahashi thinking
    *[Ask him how he knows your name]
        -> Asked
    *[Don't Interfere]
        -> DontAsk
        
=== Asked ===
It’s- #speaker:Nerd Student
Wait… how do you know my name? #speaker:Takahashi Tanjiro
You are Takahashi, aren’t you? #speaker:Nerd Student #animationBool:isTalk
Yeah, but how do you know that.#speaker:Takahashi Tanjiro 
Almost everyone would know it, except the new students of course. #speaker:Nerd Student
How come you don’t know about your own popularity… #speaker:Nerd Student #animationBool:isTalk
I’m… popular…? He’s probably joking around; April fools was yesterday. #speaker:Takahashi thinking
Not funny dude, April fools was yesterday. #speaker:Takahashi Tanjiro #speaker:Nerd Student
…Today IS 1st April #speaker:Nerd Student 
What's he talking about? it is clearly... #speaker:Takahashi thinking #animationBool:isPhoneOut
What... 1st April 2019… 2019… No way in hell this could be true… no way…
…2019… #speaker:Takahashi Tanjiro #animationBool:isPhoneIn
Ah… Takahashi, are you okay? You look pale.#speaker:Nerd Student
I ah… I’m sorry, I just didn’t sleep well. #speaker:Takahashi Tanjiro
.
~objective11Complete = true
~goToClass = true
-> END


=== DontAsk ===
It's on the first floor, first room if you take a left when you go in. #speaker:Nerd Student
Thanks! #speaker:Takahashi Tanjiro
~objective11Complete = true
-> END

