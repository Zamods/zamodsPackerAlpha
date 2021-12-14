using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;


namespace ZamodsPacker
{
    /// <summary>
    /// Class intented to generate product manifest.dsx file.
    /// </summary>
    public class Manifesto
    {
        const string MANIFESTHEADER = "<DAZInstallManifest VERSION=\"0.1\">";
        const string MANIFESTFOOTER = "</DAZInstallManifest>";

        /// <summary>
        /// Writes the product manifest file for given product. After processing all the files or sub files in content directory.
        /// </summary>
        /// <param name="path">Path of content folder where generated file will be saved.</param>
        /// <param name="globalID">Global ID of product for which manifest file is intented.</param>
        /// <returns>Completed Task upon either failure or success.</returns>
        public async Task FinalizeAndWriteToFile(string path, string globalID = "REPLACE THIS WITH ID")
        {
            try
            {
                var fileDataModels = await ReadAndProcessData(path);
                string GLOBALID = $"<GlobalID VALUE=\"{globalID}\"/>";
                string finalText = MANIFESTHEADER;
                finalText += $"\n {GLOBALID}";

                foreach (var fileDataModel in fileDataModels)
                {
                    finalText += $"\n{fileDataModel}";
                }

                finalText += $"\n{MANIFESTFOOTER}";
                string finalFilePath = $"{path}\\Manifest.dsx";

                await File.WriteAllTextAsync(finalFilePath, finalText);
                Console.WriteLine($"Wrote manifest file successfully to path: {finalFilePath}");
            }
            catch (Exception ex)
            {
                string messageBoxText = $"Failed to write file data models to manifest.\n" +
                              $"{ex.Message}";
                string caption = "Process Failed!";
                Console.WriteLine($"{messageBoxText}{caption}");
            }
            await Task.CompletedTask;
        }

        /// <summary>
        /// Reads and process the all files or sub files in content directory to be written to manifest.
        /// </summary>
        /// <param name="path">Path of content folder.</param>
        /// <returns>The list of FileDataModels that contains manifest information ready to be written.</returns>
        private async Task<List<FileDataModel>> ReadAndProcessData(string path)
        {
            var finalizedFileDataModelList = new List<FileDataModel>();
            try
            {
                List<string> directoriesPaths = new List<string>();
                List<string> filePaths = new List<string>();

                // Load all directories paths recursively
                await LoadDirectories(path, ref directoriesPaths);
                // Loads all file paths in loaded directories
                await LoadFiles(directoriesPaths, ref filePaths);
                // Converts raw file paths for manifest file
                List<string> convertedContentFilePaths = await ConvertFilePaths(filePaths);
                // Generates the manifest data structure through converted file paths
                finalizedFileDataModelList = await ConvertToFileDataModels(convertedContentFilePaths);
            }
            catch (Exception ex)
            {
                string messageBoxText = $"Failed to load file data models to write.\n" +
                              $"{ex.Message}";
                string caption = "Process Failed!";
                Console.WriteLine($"{messageBoxText}{caption}");
            }

            // Returns the fileDataModels that are ready for written to file.
            return await Task.FromResult<List<FileDataModel>>(finalizedFileDataModelList);
        }

        /// <summary>
        /// Converts the string file paths to FileDataModel for manifest file data structure.
        /// </summary>
        /// <param name="contentFilesPath">List of processed file paths in content and its sub directories.</param>
        /// <returns>The list of FileDataModels that contains manifest information ready to be written.</returns>
        private async Task<List<FileDataModel>> ConvertToFileDataModels(List<string> contentFilesPath)
        {
            var outputFilesDataModels = new List<FileDataModel>();
            try
            {
                for (int i = 0; i < contentFilesPath.Count; i++)
                {
                    var fileDataModel = new FileDataModel(value: contentFilesPath[i]);
                    outputFilesDataModels.Add(fileDataModel);
                }
            }
            catch (Exception ex)
            {
                string messageBoxText = $"Failed to convert content file paths to data models.\n" +
                              $"{ex.Message}";
                string caption = "Process Failed!";
                Console.WriteLine($"{messageBoxText}{caption}");
            }

            return await Task.FromResult<List<FileDataModel>>(outputFilesDataModels);
        }

        /// <summary>
        /// Converts the string file paths to FileDataModel for manifest file data structure.
        /// </summary>
        /// <param name="contentFilesPath">List of raw file paths in content and its sub directories.</param>
        /// <returns>The list of processed file paths that ready to generate FileDataModel.</returns>
        private async Task<List<string>> ConvertFilePaths(List<string> contentFilesPath)
        {
            var outputContentFilesPaths = new List<string>();
            try
            {
                foreach (var rawFilePath in contentFilesPath)
                {
                    var splittedRawFilePath = rawFilePath.Split("\\", StringSplitOptions.RemoveEmptyEntries);
                    var isLastContent = false;
                    var processedFilePath = "";

                    foreach (var folder in splittedRawFilePath)
                    {
                        if (isLastContent)
                        {
                            processedFilePath += $"/{folder}";
                        }

                        if (folder == "Content")
                        {
                            processedFilePath += folder;
                            isLastContent = true;
                        }
                    }
                    outputContentFilesPaths.Add(processedFilePath);
                }

            }
            catch (Exception ex)
            {
                string messageBoxText = $"Failed to convert content file paths.\n" +
                              $"{ex.Message}";
                string caption = "Process Failed!";
                Console.WriteLine($"{messageBoxText}{caption}");
            }

            return await Task.FromResult<List<string>>(outputContentFilesPaths);
        }

        /// <summary>
        /// Loads all the file paths in provided list of directories.
        /// </summary>
        /// <param name="directoryPaths">List of directories in which intended files exists for manifest.</param>
        /// <param name="filePaths">List of raw file paths that is returned.</param>
        /// <returns>The list of all raw file paths that exists in each and every single directory in content directory.</returns>
        private Task LoadFiles(List<string> directoryPaths, ref List<string> filePaths)
        {
            foreach (var directory in directoryPaths)
            {
                var files = Directory.GetFiles(directory);
                foreach (var file in files)
                {
                    filePaths.Add(file);
                }
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Loads all the directories and its sub directories recursively.
        /// </summary>
        /// <param name="path">Path of directory in which other directories might exists.</param>
        /// <param name="directoryPaths">List of all directories that has been loaded is returned.</param>
        /// <returns>The list of all directories that exists in each and every single directory in content directory.</returns>
        private Task LoadDirectories(string path, ref List<string> directoryPaths)
        {
            var directories = Directory.GetDirectories(path);
            foreach (var directory in directories)
            {
                directoryPaths.Add(directory);
                LoadDirectories(directory, ref directoryPaths);
            }

            return Task.CompletedTask;
        }
    }
}
