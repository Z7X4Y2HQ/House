INCLUDE Globals.ink 
{
	- currentObjective < 5:
		I don't have time for this.#speaker:Takahashi Tanjiro #animationBool:isWalk
	- currentObjective > 6 || currentObjective < 9:
		I have a Tshirt and my school uniform.#speaker:Takahashi Tanjiro #animationBool:isWalk
		Enough for a teenager!
		Where's my favourite jacket?
	- else:
        I have a Tshirt and my school uniform.#speaker:Takahashi Tanjiro #animationBool:isWalk
		Enough for a teenager!
}
