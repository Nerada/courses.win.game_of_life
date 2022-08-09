// -----------------------------------------------
//     Author: Ramon Bollen
//      File: GameOfLife.PatternLib.cs
// Created on: 20220809
// -----------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GameOfLife;

public static class PatternLib
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public static readonly IReadOnlyDictionary<string, Pattern> Patterns = new Dictionary<string, Pattern>
    {
        {
            "Ramon", new Pattern("Ramon", 27, 8, 10, @"
            4o$o3bo$o3bo2b3o2b2ob2o2b2o3b3o$o3bo4bobo2bobobo2bobo2bo$
            4o3b3obo2bobobo2bobo2bo$o2bo2bo2bobo2bobobo2bobo2bo$
            o3bobo2bobo2bobobo2bobo2bo$o3bo2b3obo2bobo2b2o2bo2bo!")
        },
        {
            "2-engine Cordership", new Pattern("2-engine Cordership", 41, 49, 20, @"
            19b2o$19b4o$19bob2o2$20bo$19b2o$19b3o$21bo$33b2o$33b2o7$36bo$35b2o$34b
            o3bo$35b2o2bo$40bo$37bobo$38bo$38bo$38b2o$38b2o3$13bo10bo$12b5o5bob2o
            11bo$11bo10bo3bo9bo$12b2o8b3obo9b2o$13b2o9b2o12bo$2o13bo21b3o$2o35b3o
            7$8b2o$8b2o11b2o$19b2o2bo$24bo3bo$18bo5bo3bo$19bo2b2o3bobo$20b3o5bo$
            28bo!", new Uri("https://conwaylife.com/patterns/2enginecordership.rle"))
        }
    };
}