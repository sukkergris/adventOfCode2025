module Tests

open System
open Xunit
open AdventOfCode.DeScrambler

[<Fact>]
let ``Load data from txt`` () =

    let x = loadLines2Array "TestData/x.txt"
    Assert.Equal<int>(10, x.Length)

[<Fact>]
let ``From string to direction`` () =
    let left = "L123" |> string2Direction
    let right = "R321" |> string2Direction
    Assert.Equal<Direction>(Left 123, left)
    Assert.Equal(Right 321, right)

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
    let result = calculateNewDialPositionAfterMove start direction
    Assert.Equal(expected, result)

[<Fact>]
let ``Dial left`` () =
    let direction = Left 20
    Assert.True(
        match direction with
        | Left _ -> true
        | _ -> false)
