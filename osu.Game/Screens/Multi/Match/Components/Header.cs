﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Beatmaps;
using osu.Game.Beatmaps.Drawables;
using osu.Game.Graphics;
using osu.Game.Graphics.UserInterface;
using osu.Game.Online.Multiplayer;
using osu.Game.Overlays.SearchableList;
using osuTK.Graphics;

namespace osu.Game.Screens.Multi.Match.Components
{
    public class Header : Container
    {
        public const float HEIGHT = 200;

        public readonly IBindable<WorkingBeatmap> Beatmap = new Bindable<WorkingBeatmap>();

        private readonly Box tabStrip;

        public readonly MatchTabControl Tabs;

        public Action OnRequestSelectBeatmap;

        public Header()
        {
            RelativeSizeAxes = Axes.X;
            Height = HEIGHT;

            BeatmapSelectButton beatmapButton;
            UpdateableBeatmapBackgroundSprite background;
            Children = new Drawable[]
            {
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Masking = true,
                    Child = background = new HeaderBeatmapBackgroundSprite { RelativeSizeAxes = Axes.Both }
                },
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = ColourInfo.GradientVertical(Color4.Black.Opacity(0), Color4.Black.Opacity(0.5f)),
                },
                tabStrip = new Box
                {
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.BottomLeft,
                    RelativeSizeAxes = Axes.X,
                    Height = 1,
                },
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding { Horizontal = SearchableListOverlay.WIDTH_PADDING },
                    Children = new Drawable[]
                    {
                        new Container
                        {
                            Anchor = Anchor.TopRight,
                            Origin = Anchor.TopRight,
                            RelativeSizeAxes = Axes.Y,
                            Width = 200,
                            Padding = new MarginPadding { Vertical = 5 },
                            Child = beatmapButton = new BeatmapSelectButton
                            {
                                RelativeSizeAxes = Axes.Both,
                                Height = 1
                            },
                        },
                        Tabs = new MatchTabControl
                        {
                            Anchor = Anchor.BottomLeft,
                            Origin = Anchor.BottomLeft,
                            RelativeSizeAxes = Axes.X
                        },
                    },
                },
            };

            beatmapButton.Action = () => OnRequestSelectBeatmap?.Invoke();

            background.Beatmap.BindTo(Beatmap);
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            tabStrip.Colour = colours.Yellow;
        }

        private class BeatmapSelectButton : TriangleButton
        {
            private readonly IBindable<int?> roomIDBind = new Bindable<int?>();

            [Resolved]
            private Room room { get; set; }

            public BeatmapSelectButton()
            {
                Text = "Select beatmap";
            }

            [BackgroundDependencyLoader]
            private void load()
            {
                roomIDBind.BindTo(room.RoomID);
                roomIDBind.BindValueChanged(v => Enabled.Value = !v.HasValue, true);
            }
        }

        private class HeaderBeatmapBackgroundSprite : UpdateableBeatmapBackgroundSprite
        {
            protected override double FadeDuration => 0;
        }
    }
}
