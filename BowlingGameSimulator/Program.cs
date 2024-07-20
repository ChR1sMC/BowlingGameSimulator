using Application.Services;
using BowlingGameSimulator;

var bowlingGame = new BowlingGame();
var bowlingGameRandomSimulator = new BowlingGameRandomSimulator(bowlingGame);
var roundsScores = bowlingGameRandomSimulator.SimulateGame();

foreach(var roundScore in roundsScores)
{
    Console.WriteLine($"Hit {roundScore}");
}

Console.WriteLine($"Final score: {bowlingGame.Score()}");

