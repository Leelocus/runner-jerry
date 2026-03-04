# Architecture

## Core Loop
1. Player auto-runs forward.
2. Player can switch between 3 lanes.
3. Player can jump over obstacles.
4. Colliding with a lethal obstacle ends the run.
5. Track segments are recycled to simulate an endless road.
6. Controller tracks distance score and collected coins.

## Systems

### RunnerGameController
Owns game state:
- running / game over
- distance score
- coin count
- restart hooks

### RunnerPlayerController
Owns player movement:
- forward velocity
- lane target
- jump arc
- collision response

### TrackLoopSpawner
Owns endless level flow:
- initial segment spawn
- current active segment list
- recycle when player passes segment exit point

### TrackSegment
Minimal data carrier for a reusable segment:
- segment length
- exit marker

### SceneActions
Minimal UI bridge for menu buttons.

## Why this is V1
The original project mixes old and new controller styles and contains package/demo residue.
This version starts from a cleaner foundation so later iterations can add polish without carrying old noise.
