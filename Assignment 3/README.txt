Zak Olyarnik
CS-345
Assignment 3

Pac-Man reimagining: Navigate your spaceship through the asteroid field, collecting stars but avoiding UFOs.  Supernovas will temporarily freeze the enemies, but don't get hit
	or you'll shrink!

Move - Arrow keys

All art assets are original Photoshop creations.
Font is OCRA Std.

This game makes use of basic enemy AI, taken from the Week 3 in-class tutorial but modified to better fit my needs.  The UFOs needed to detect the player from all directions, 
	for example, rather than just the "forward" direction the tutorial used.  So I created a trigger collider with a much larger area than the sprite, to act as a "field of
	view", allowing me to eliminate the raycasting.  I also simplified the provided code by changing the enemy sprites directly rather than passing in an animator.  If the
	player enters a UFO's trigger collider, the UFO goes into "chase" mode and emits a red aura.  The player can still run away at this point, but if they come into direct 
	contact with the UFO, they will shrink and lose points as long as they remain in contact.  If the player does manage to escape, the UFO goes back into patrol mode.  
	Picking up a supernova puts the UFOs into "freeze" mode, from which they emit a blue aura and cannot move for a few seconds.  Asteroids are just static obstacles, and all 
	stars must be collected to finish the game.
Additional improvements I made include adding UI, a simple start menu (and passing the score to it in between games), and making sure moving objects cannot leave the screen.  
	There were several sticking points throughout development, but I found workarounds for all of them and am satisfied with the final game.