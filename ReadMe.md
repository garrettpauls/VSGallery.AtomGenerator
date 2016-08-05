# VSGallery.AtomGenerator

This program generates an atom.xml file for hosting
a [private Visual Studio gallery](https://msdn.microsoft.com/en-us/library/hh266717.aspx).

# Basic Usage

Place the executable and dependencies in the same folder
as the vsix files you want to host, then run it.
It will automatically generate an atom.xml file which can
be referenced as a gallery in visual studio.

To update packages simply replace the vsix file or
add a copy with the new version, then run the executable again.
It will automatically regenerate the atom.xml file with 
information from the highest version of each vsix file.

To add a custom atom gallery in Visual Studio go to
Tools>Options>Environment>Extensions and Updates, then
add a new gallery with its URL pointing to the 
generated atom.xml file.

# Details

The root directory is where the atom.xml file will be generated.
Any vsix files in the root directory or subdirectories will be included.

If any arguments are passed to the executable, the first
one representing an existing directory will be used as the
root directory instead of the current working directory.

If any vsix files contain icons or preview images, these will
be extracted to a subfolder of the root directory named VSIXImages.

If any vsix files do not contain a valid manifest or any other
errors occur, they will be logged to a file in the log subdirectory.

# Changelog

The changelog of all versions.
Issue details can be found by number in the [issue tracker](https://github.com/garrettpauls/VSGallery.AtomGenerator/issues).

## v1.0.2

* Pulled in ChrisMaddock's changes:
  * Fixed URI format exceptions when targeting network paths.
  * Added return code 0 for success, 1 for error.
* Logger no longer throws an exception if it can't write to the file.

## v1.0.1

* Issue #1 - zip entry names for images are normalized to support both / and \ in the manifest file.
* Issue #2 - added more verbose logging and output to stdout.

## v1.0.0

* Initial release with base functionality.
* Generates atom.xml based on .vsix files in the supplied directory.
* Supports both PackageManifest and Vsix manifest based files.
* Extracts icons and preview images from .vsix files.