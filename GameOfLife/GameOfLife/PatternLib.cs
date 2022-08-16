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
            "Ramon", new Pattern("Ramon", 31, 8, 16, @"
            4o$o3bo$o3bo3b3o3b2ob2o3b2o4b3o$o3bo5bo2bo2bobo2bo2bo2bo2bo$
            4o4b3o2bo2bobo2bo2bo2bo2bo$o2bo3bo2bo2bo2bobo2bo2bo2bo2bo$
            o3bo2bo2bo2bo2bobo2bo2bo2bo2bo$o3bo3b3o2bo2bobo3b2o3bo2bo!")
        },
        {
            "2-engine Cordership", new Pattern("2-engine Cordership", 41, 49, 20, @"
            19b2o$19b4o$19bob2o2$20bo$19b2o$19b3o$21bo$33b2o$33b2o7$36bo$35b2o$34b
            o3bo$35b2o2bo$40bo$37bobo$38bo$38bo$38b2o$38b2o3$13bo10bo$12b5o5bob2o
            11bo$11bo10bo3bo9bo$12b2o8b3obo9b2o$13b2o9b2o12bo$2o13bo21b3o$2o35b3o
            7$8b2o$8b2o11b2o$19b2o2bo$24bo3bo$18bo5bo3bo$19bo2b2o3bobo$20b3o5bo$
            28bo!", new Uri("https://conwaylife.com/patterns/2enginecordership.rle"))
        },
        {
            "simkinsp60", new Pattern("simkinsp60", 33, 31, 10, @"
            22bo$13b2o5b3o$14bo4bo$14bob2o2bo$15bo5bo$16bo3b2o$17bo2$10bo$2o3bo4b
            2o$2o2b2o5b2o$5b2o4bo$6bo3bo6$22bo3bo$21bo4b2o$20b2o5b2o2b2o$21b2o4bo
            3b2o$22bo2$15bo$11b2o3bo$11bo5bo$12bo2b2obo$13bo4bo$10b3o5b2o$10bo
            !", new Uri("https://conwaylife.com/patterns/simkinsp60.rle"))
        }
    };
}