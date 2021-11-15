﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using osu.Game.Rulesets.Difficulty.Skills;
using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Difficulty
{
    public class DifficultyAttributes
    {
        [JsonIgnore]
        public Mod[] Mods { get; set; }

        [JsonIgnore]
        public Skill[] Skills { get; set; }

        [JsonProperty("star_rating")]
        public double StarRating { get; set; }

        [JsonProperty("max_combo")]
        public int MaxCombo { get; set; }

        public DifficultyAttributes()
        {
        }

        public DifficultyAttributes(Mod[] mods, Skill[] skills, double starRating)
        {
            Mods = mods;
            Skills = skills;
            StarRating = starRating;
        }

        public virtual IEnumerable<(int attributeId, object value)> ToDatabase() => Enumerable.Empty<(int, object)>();

        public virtual void FromDatabase(IReadOnlyDictionary<int, double> values, int hitCircleCount, int spinnerCount)
        {
        }
    }
}
