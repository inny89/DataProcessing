﻿module FSharp.TV.Trie

    type NodeFlag =
        | EndOfWord
        | IncompleteWord

    type Trie =
        { children : Map<char, Trie>
          flag : NodeFlag }

    let emptyTrie =
        { children = Map.empty
          flag = IncompleteWord }

    let insertWord word initialTrie =
        let rec innerInsertChars charList currentTrie =
            match charList with
            | [] -> { emptyTrie with flag = EndOfWord }
            | ch::rest ->
                let innerMap = currentTrie.children
                let innerTrie =
                    match innerMap |> Map.tryFind ch with
                    | None -> emptyTrie
                    | Some childTrie -> childTrie
                let newInnerTrie = innerInsertChars rest innerTrie
                let newMap = innerMap |> Map.add ch newInnerTrie
                { currentTrie with children = newMap }
        innerInsertChars (word |> Seq.toList) initialTrie