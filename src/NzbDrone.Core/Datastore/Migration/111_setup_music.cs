﻿using FluentMigrator;
using NzbDrone.Core.Datastore.Migration.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NzbDrone.Core.Datastore.Migration
{
    [Migration(111)]
    public class setup_music : NzbDroneMigrationBase
    {
        protected override void MainDbUpgrade()
        {
            Create.TableForModel("Artists")
                .WithColumn("ItunesId").AsInt32().Unique()
                .WithColumn("ArtistName").AsString().Unique()
                .WithColumn("ArtistSlug").AsString().Unique()
                .WithColumn("CleanTitle").AsString()
                .WithColumn("Monitored").AsBoolean()
                .WithColumn("LastInfoSync").AsDateTime().Nullable()
                .WithColumn("LastDiskSync").AsDateTime().Nullable()
                .WithColumn("Overview").AsString()
                .WithColumn("Status").AsInt32()
                .WithColumn("Path").AsString()
                .WithColumn("Images").AsString()
                .WithColumn("QualityProfileId").AsInt32()
                .WithColumn("AirTime").AsString().Nullable() // JVM: This might be DropDate instead
                //.WithColumn("BacklogSetting").AsInt32()
                ;

            Create.TableForModel("Albums")
                .WithColumn("AlbumId").AsInt32() // Does this map to collectionId?
                .WithColumn("CompilationId").AsInt32()
                .WithColumn("Compilation").AsBoolean()
                .WithColumn("Title").AsString()
                .WithColumn("Year").AsInt32()
                .WithColumn("Image").AsInt32() // Is this needed?
                .WithColumn("TrackCount").AsInt32()
                .WithColumn("DiscCount").AsInt32()
                .WithColumn("Monitored").AsBoolean();

            Create.TableForModel("Tracks")
                .WithColumn("ItunesTrackId").AsInt32().Unique()
                .WithColumn("AlbumId").AsInt32()
                .WithColumn("CompilationId").AsInt32().Nullable()
                .WithColumn("Compilation").AsBoolean().WithDefaultValue("False")
                .WithColumn("TrackNumber").AsInt32()
                .WithColumn("Title").AsString().Nullable()
                .WithColumn("Ignored").AsBoolean().Nullable()
                .WithColumn("Explict").AsBoolean()
                .WithColumn("TrackExplicitName").AsString().Nullable()
                .WithColumn("TrackCensoredName").AsString().Nullable()
                .WithColumn("TrackFileId").AsInt32().Nullable()
                .WithColumn("ReleaseDate").AsDateTime().Nullable();
            //.WithColumn("AbsoluteEpisodeNumber").AsInt32().Nullable()
            //.WithColumn("SceneAbsoluteEpisodeNumber").AsInt32().Nullable()
            //.WithColumn("SceneSeasonNumber").AsInt32().Nullable()
            //.WithColumn("SceneEpisodeNumber").AsInt32().Nullable();

            Create.TableForModel("TrackFiles")
                  .WithColumn("ArtistId").AsInt32()
                  .WithColumn("Path").AsString().Unique()
                  .WithColumn("Quality").AsString()
                  .WithColumn("Size").AsInt64()
                  .WithColumn("DateAdded").AsDateTime()
                  .WithColumn("AlbumId").AsInt32(); // How does this impact stand alone tracks?

            Create.TableForModel("Compilation")
                   .WithColumn("CompilationId").AsInt32().Unique()
                   .WithColumn("ArtistId").AsString().Nullable();
        }

    }
}
