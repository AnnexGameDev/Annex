﻿using Annex.Events;
using Annex.Graphics;
using Annex.IO.Hashing;
using Annex.Logging;
using System;
using System.Diagnostics;
using System.IO;

namespace Annex
{
    public class Game
    {
        public void Start() {
            Singleton.Create<Log>();
            this.CopyResources();
            var events = Singleton.Create<EventQueue>();
            var window = Singleton.Create<GameWindow>();

            events.AddEvent(PriorityType.GRAPHICS, () => {
                window.Context.BeginDrawing();

                window.Context.EndDrawing();
                return ControlEvent.NONE;
            }, 16, 0);

            window.Context.SetVisible(true);
            events.Run();
        }

        [Conditional("DEBUG")]
        private void CopyResources() {
            var log = Singleton.Get<Log>();

            var di = new DirectoryInfo(".");
            string solutionPath;
            while (true) {
                if (Directory.GetFiles(di.FullName, "*.sln").Length != 0) {
                    solutionPath = di.FullName;
                    break;
                }
                di = di.Parent;
            }
            string resourcePath = Path.Combine(solutionPath, "resources");

            log.WriteLine($"SolutionPath: {solutionPath}");
            log.WriteLine($"ResourcePath: {resourcePath}");
            log.WriteLine($"Creating directory: {resourcePath}. Exists: {Directory.Exists(resourcePath)}");
            Directory.CreateDirectory(resourcePath);

            using (var md5 = new MD5()) {
                foreach (var sourceFile in Directory.GetFiles(resourcePath, "*", SearchOption.AllDirectories)) {
                    log.WriteLine($"Copying file {sourceFile}.");
                    var fi = new FileInfo(sourceFile);
                    string relativeDirectory = fi.Directory.FullName.Remove(0, solutionPath.Length + 1);
                    string relativeFilePath = Path.Combine(relativeDirectory, fi.Name);

                    log.WriteLine($"Relative Directory: {relativeDirectory}");
                    log.WriteLine($"Relative File Path: {relativeFilePath}");
                    log.WriteLine($"Creating directory: {relativeDirectory}. Exists: {Directory.Exists(relativeDirectory)}");
                    Directory.CreateDirectory(relativeDirectory);

                    log.WriteLine($"Resource file exists: {File.Exists(relativeFilePath)}");
                    if (File.Exists(relativeFilePath)) {
                        string sourceFileHash = md5.ComputeFileHash(sourceFile);
                        string relativeFileHash = md5.ComputeFileHash(relativeFilePath);

                        log.WriteLine($"MD5 Hash for {sourceFile}: {sourceFileHash}");
                        log.WriteLine($"MD5 Hash for {relativeFilePath}: {relativeFileHash}");

                        if (sourceFileHash == relativeFileHash) {
                            log.WriteLine("Duplicate file detected. Skipping.");
                            continue;
                        }

                        // There is a conflict. Create a backup of the old file just in case.
                        string backupFile = Path.Combine(relativeDirectory, relativeFileHash + '_' + fi.Name);
                        log.WriteLine($"Backup file {backupFile} exists: {File.Exists(backupFile)}");
                        Debug.Assert(!File.Exists(backupFile));

                        log.WriteLine($"Creating backup file: {backupFile}");
                        File.Copy(relativeFilePath, backupFile);
                    }

                    log.WriteLine($"Copying resource file {sourceFile} to {relativeFilePath}");
                    File.Copy(sourceFile, relativeFilePath, true);
                }
            }
        }
    }
}
