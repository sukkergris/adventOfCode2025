namespace AdventOfCode

module FileHandler =
    let loadLinesFromFile(path: string) : string list =
        System.IO.File.ReadAllLines path |> Array.toList

    let loadCSVFile(path: string) : string list =
        let lines = System.IO.File.ReadAllLines path |> Array.toList
        lines.Head.Split(',') |> Array.toList

    let saveToFile(path: string) list =
        let content = list
                            |> List.map string
                            |> List.toSeq
        System.IO.File.WriteAllLines(path, content)

