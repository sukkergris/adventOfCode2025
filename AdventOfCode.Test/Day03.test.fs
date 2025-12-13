module Day03.test

open Xunit
open AdventOfCode.Lobby

[<Fact>]
let ``Answer from extended test data`` () =
    let answer = maxJoltageUsingNBatteries "TestData/day03.01.extended-test.txt" 2
    Assert.Equal(478, answer)

[<Fact>]
let ``Answer from test data`` () =
    let answer = maxJoltageUsingNBatteries "TestData/day03.01.test.txt" 2
    Assert.Equal(357, answer)

[<Fact>]
let ``Answer for level 1`` () =
    let answer = maxJoltageUsingNBatteries "TestData/day03.01.txt" 2
    Assert.Equal(17403, answer)

[<Fact>]
let ``Answer for level 2 using 2 batteries`` () =
    let answer = maxJoltageUsingNBatteries "TestData/day03.01.txt" 2
    Assert.Equal(478, answer)
