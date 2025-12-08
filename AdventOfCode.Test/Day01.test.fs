module Day01.test

open Xunit
open AdventOfCode.DeScrambler

[<Fact>]
let ``Load data from txt`` () =

    let x = loadLinesFromFile "TestData/x.txt"
    Assert.Equal<int>(10, x.Length)

[<Fact>]
let ``From string to direction`` () =
    let left = "L123" |> parseDirection
    let right = "R321" |> parseDirection
    Assert.Equal<Direction>(Left 123, left)
    Assert.Equal(Right 321, right)

[<Theory>]
[<InlineData(0, 0)>]
[<InlineData(1, 1)>]
[<InlineData(50, 50)>]
[<InlineData(99, 99)>]
[<InlineData(100, 0)>]
[<InlineData(101, 1)>]
[<InlineData(150, 50)>]
[<InlineData(200, 0)>]
[<InlineData(321, 21)>]
[<InlineData(-1, 99)>]
[<InlineData(-50, 50)>]
[<InlineData(-99, 1)>]
[<InlineData(-100, 0)>]
[<InlineData(-101, 99)>]
[<InlineData(-150, 50)>]
[<InlineData(-200, 0)>]
[<InlineData(-1004, 96)>]
let ``toDialPosition wraps any integer to valid dial range`` (input: int, expected: int) =
    let result = toDialPosition input
    Assert.Equal(expected, result)

[<Theory>]
[<InlineData(50, -20, false)>]
[<InlineData(50,20, false)>]
[<InlineData(99,1,false)>]
[<InlineData(99,2, true)>]
[<InlineData(1, -2, true)>]
[<InlineData(0, -1, false)>]
[<InlineData(0, -99, false)>]
[<InlineData(0,-100, false)>]
let ``Didn't pass zero`` (start: int, move: int, expected: bool) =
    let direction =
        if move >= 0 then move |> Right else -move |> Left
    let didParseZeroWhileMove = crossingZero start direction
    Assert.Equal(expected, didParseZeroWhileMove)

[<Theory>]
[<InlineData(50, 50, 0)>]
[<InlineData(50, 150, 0)>]
[<InlineData(50, -50, 0)>]
[<InlineData(50, -150, 0)>]
[<InlineData(99, 1, 0)>]
[<InlineData(99, 101, 0)>]
[<InlineData(1, -1, 0)>]
[<InlineData(1, -101, 0)>]
[<InlineData(0, 0, 0)>]
[<InlineData(0, 1000, 0)>]
let ``Ensure we always end on 0 on a 100 sized dial`` (start:int, move:int, expected:int) =
    let direction =
        if move >= 0 then move |> Right else -move |> Left
    let result = calculateNewDialPositionAfterMove start direction 0
    Assert.Equal(expected, result.Position)

[<Fact>]
let ``Calculate new dial position after move`` () =
    let rm91 = (10, Right 91, 0) |||> calculateNewDialPositionAfterMove
    Assert.Equal(1, rm91.Position)
    let lm91 = (10, Left 91, 0) |||> calculateNewDialPositionAfterMove
    Assert.Equal(19, lm91.Position)

[<Fact>]
let ``Calculate new dial position after move from example`` () =
    let L68 = (50, Left 68, 0) |||> calculateNewDialPositionAfterMove
    Assert.Equal(82, L68.Position)
    Assert.Equal(1, L68.Rounds)
    let L30 = (L68.Position, Left 30, L68.Rounds) |||> calculateNewDialPositionAfterMove
    Assert.Equal( 52, L30.Position)
    Assert.Equal(1, L30.Rounds)
    let R48 = (L30.Position, Right 48, L30.Rounds) |||> calculateNewDialPositionAfterMove
    Assert.Equal(0, R48.Position)
    Assert.Equal(1, R48.Rounds)
    let L5 = (R48.Position, Left 5, R48.Rounds) |||> calculateNewDialPositionAfterMove
    Assert.Equal(95, L5.Position)
    Assert.Equal(1, L5.Rounds)
    let R60 = (L5.Position, Right 60, L5.Rounds) |||> calculateNewDialPositionAfterMove
    Assert.Equal(55, R60.Position)
    Assert.Equal(2, R60.Rounds)

[<Theory>]
[<InlineData(10, 91, 0, 1, 1)>]
[<InlineData(50, 50, 0, 0, 0)>]
[<InlineData(99, 1, 0, 0, 0)>]
[<InlineData(99, 2, 0, 1, 1)>]
[<InlineData(0, 100, 0, 0, 1)>]
[<InlineData(50, 150, 0, 0, 1)>]
let ``getNextDialPosition moves right correctly`` (start: int, move: int, initialRounds: int, expectedPos: int, expectedRounds: int) =
    let result = getNextDialPosition start (Right move) initialRounds
    Assert.Equal(expectedPos, result.Position)
    Assert.Equal(expectedRounds, result.Rounds)

[<Theory>]
[<InlineData(10, 91, 0, 19, 1)>]
[<InlineData(50, 50, 0, 0, 0)>]
[<InlineData(1, 1, 0, 0, 0)>]
[<InlineData(1, 2, 0, 99, 1)>]
[<InlineData(0, 1, 0, 99, 0)>]
[<InlineData(50, 150, 0, 0, 1)>]
let ``getNextDialPosition moves left correctly`` (start: int, move: int, initialRounds: int, expectedPos: int, expectedRounds: int) =
    let result = getNextDialPosition start (Left move) initialRounds
    Assert.Equal(expectedPos, result.Position)
    Assert.Equal(expectedRounds, result.Rounds)

[<Fact>]
let ``getNextDialPosition tracks rounds through multiple moves`` () =
    let move1 = getNextDialPosition 50 (Left 68) 0
    Assert.Equal(82, move1.Position)
    Assert.Equal(1, move1.Rounds)

    let move2 = getNextDialPosition move1.Position (Left 30) move1.Rounds
    Assert.Equal(52, move2.Position)
    Assert.Equal(1, move2.Rounds)

    let move3 = getNextDialPosition move2.Position (Right 48) move2.Rounds
    Assert.Equal(0, move3.Position)
    Assert.Equal(1, move3.Rounds)


