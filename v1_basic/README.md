# V1 Basic Runner

This folder is the first baseline version for rebuilding the endless runner step by step.

Scope of V1:
- 3-lane runner foundation
- forward auto-run
- lane switch
- jump arc
- obstacle death hook
- segment loop architecture
- simple score and coin state

This is intentionally a lightweight architecture version, not a full asset-complete Unity project.
The existing `Endless-Runner-3D-game-master` folder remains the frozen final reference.

## Folder Layout
- `Scripts/RunnerGameController.cs`: score, coins, game state
- `Scripts/RunnerPlayerController.cs`: forward movement, lane changes, jump, death
- `Scripts/TrackSegment.cs`: reusable segment descriptor
- `Scripts/TrackLoopSpawner.cs`: spawn and recycle level chunks
- `Scripts/SceneActions.cs`: scene/menu actions
- `ARCHITECTURE.md`: how the systems fit together
- `ROADMAP.md`: planned V1 -> V7 evolution path

## V1 Goal
V1 is the clean starting point for later upgrades, not the final gameplay version.
