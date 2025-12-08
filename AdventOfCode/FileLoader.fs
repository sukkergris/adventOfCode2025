namespace AdventOfCode

module FileLoader =
    let loadLinesFromFile(path: string) : string list =
        System.IO.File.ReadAllLines path |> Array.toList

    let loadCSVFile(path: string) : string list =
        let lines = System.IO.File.ReadAllLines path |> Array.toList
        lines.Head.Split(',') |> Array.toList

