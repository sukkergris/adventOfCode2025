module Day04.test

open Xunit
open AdventOfCode.PrintingDepartment

[<Fact>]
let ``Print the stuff`` () =
    let board = "TestData/day04.test.txt"
                |> getInput
                |> toBoard
    Assert.Equal(10,board.Length)

[<Theory>]
[<InlineData(0,0,3,3,3)>]
[<InlineData(1,0,3,3,5)>]
[<InlineData(1,1,3,3,8)>]
[<InlineData(0,2,3,3,3)>]
let ``Get adjacent coords to point`` (x: int, y:int, abscissa:int, ordinate:int, expected: int) =
    let p = {x=x;y=y; c='@'}
    let adjacentCoords = getAdjacentCoords p abscissa ordinate
    // printfn "%A" adjacentCoords
    Assert.Equal(expected, adjacentCoords.Length)

[<Fact>]
let ``Can lift status`` () =
    let board = "TestData/day04.test.txt"
                |> getInput
                |> toBoard
    let firstLine = board[0]
    let updatedCell = ({x=7;y=0;c='@'},board) ||> getLiftAbility
    Assert.Equal(updatedCell.c, '@')

[<Fact(Skip="Just for printing to compare")>]
let ``Updated board`` () =
    let updatedBoard = "TestData/day04.test.txt"
                    |> getInput
                    |> toBoard
                    |> getUpdatedBoard
    let referenceBoard = "TestData/day04.test.result.txt"
                        |> getInput
                        |> toBoard

    // Print side-by-side comparison
    printfn "\n%s | %s" "Expected" "Actual"
    printfn "%s-+-%s" (String.replicate 10 "-") (String.replicate 10 "-")
    List.zip referenceBoard updatedBoard
    |> List.iter (fun (refRow, updRow) ->
        let refChars = refRow |> List.map (fun p -> p.c) |> List.toArray |> System.String
        let updChars = updRow |> List.map (fun p -> p.c) |> List.toArray |> System.String
        let marker = if refChars = updChars then " " else "*"
        printfn "%s | %s %s" refChars updChars marker)

    let flatUpdated = updatedBoard |> List.concat
    let flatReference = referenceBoard |> List.concat

    // Print individual differences
    let diffs =
        List.zip flatReference flatUpdated
        |> List.filter (fun (expected, actual) -> expected <> actual)

    if not diffs.IsEmpty then
        printfn "\nDifferences:"
        diffs |> List.iter (fun (expected, actual) ->
            printfn "  (%d,%d): '%c' → '%c'"
                expected.x expected.y expected.c actual.c)

    Assert.Equal<point list>(flatReference, flatUpdated)

[<Fact>]
let ``Count fork-liftable test`` () =
    let updated = "TestData/day04.test.txt"
                    |> getInput
                    |> toBoard
                    |> getUpdatedBoard
                    |> List.concat
    let countForkLiftable = updated
                            |> List.filter(fun x -> x.c = 'x')
                            |> List.length
    Assert.Equal(13, countForkLiftable)

    let countRolles = updated
                        |> List.filter(fun x-> x.c = '@')
                        |> List.length
    Assert.Equal(58,countRolles)



[<Fact>]
let ``Count fork-liftable first trail`` () =
    let updated = "TestData/day04.01.txt"
                    |> getInput
                    |> toBoard
                    |> getUpdatedBoard
                    |> List.concat
    let count = updated
                    |> List.filter(fun x -> x.c = 'x')
                    |> List.length
    Assert.Equal(1489, count)

[<Fact>]
let ``tryFindPointsWithSupport returns Some when support point exists`` () =
    let board = [
        [ { x = 0; y = 0; c = '.' }; { x = 1; y = 0; c = '@' } ]
        [ { x = 0; y = 1; c = '.' }; { x = 1; y = 1; c = '.' } ]
    ]
    let coord = { x = 1; y = 0 }
    let result = tryFindPointsWithSupport board coord
    Assert.True(result.IsSome)
    Assert.Equal('@', result.Value.c)

[<Fact>]
let ``tryFindPointsWithSupport returns None when no support point exists`` () =
    let board = [
        [ { x = 0; y = 0; c = '.' }; { x = 1; y = 0; c = '.' } ]
        [ { x = 0; y = 1; c = '.' }; { x = 1; y = 1; c = '.' } ]
    ]
    let coord = { x = 1; y = 0 }
    let result = tryFindPointsWithSupport board coord
    Assert.True(result.IsNone)
