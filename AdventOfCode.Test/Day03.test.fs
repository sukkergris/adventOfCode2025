module Day03.test

open Xunit
open AdventOfCode.Lobby

[<Fact>]
let ``Answer from extended test data`` () =
    let answer = maxJoltageUsingNBatteriesTheodor "TestData/day03.01.extended-test.txt" 2
    Assert.Equal(478L, answer)

[<Fact>]
let ``Answer from test data`` () =
    let answer = maxJoltageUsingNBatteriesTheodor "TestData/day03.01.test.txt" 2
    Assert.Equal(357L, answer)

[<Theory>]
[<InlineData(2,17403L)>]
[<InlineData(12,173416889848394L)>]
let ``Answer for level 1`` (n,expected) =
    let answer = maxJoltageUsingNBatteriesTheodor "TestData/day03.01.txt" n
    Assert.Equal(expected, answer)

[<Theory>]
[<InlineData("987654321111111",98L)>]
[<InlineData("811111111111119",89L)>]
[<InlineData("234234234234278",78L)>]
[<InlineData("818181911112111",92L)>]
[<InlineData("111113311111111",33L)>]
[<InlineData("441113311111144",44L)>]
[<InlineData("111113311111155",55L)>]
let ``Answer for level 2 using 2 batteries`` (input: string, expected: int64) =
    let v = input
            |> strArr2NumArr
            |> getVal 2

    Assert.Equal(expected=expected, actual = v)

[<Fact>]
let ``Answer using 3 batteries`` () =
    let batteries = "1987654321111111"

    let v = batteries
            |> strArr2NumArr
            |> getVal 3

    Assert.Equal(expected=987L, actual = v)

[<Fact>]
let ``Answer using 4 batteries`` () =
    let batteries = "1987654321111111"

    let v = batteries
            |> strArr2NumArr
            |> getVal 4

    Assert.Equal(expected=9876L, actual = v)

[<Fact>]
let ``Answer using 12 batteries`` () =
    let batteries = "66655456766266656565654545666566746526656565329434656545665565566665566654446966665656769876543219871"

    let v = batteries
            |> strArr2NumArr
            |> getVal 12
    let ciffersInV = v |> abs |> string |> String.length
    Assert.Equal(expected=12, actual = ciffersInV)

