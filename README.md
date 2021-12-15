
 ![zamodsPacker](https://am3pap003files.storage.live.com/y4mHSveaj4d2ea6dC7RqcOYtgHQS_Yfwxgf8EaFib4SbUWf2j9DPKf6VwLE5-3-UUH0MGBuaWEp6kBq0AmaPA8plZNPqUVBpE2pPtf80ztehn4afqpwIHVQy9f4j3lNyDr4Lps0OR-cYsMt7B4Wsv7w_wORe39G_ICRs-ymo_Bs-5QC6eeA0C2-Npn3vUrIo-jG?width=512&height=256&cropmode=none)
# zamodsPacker
A console utility to generate **DIM** ready package names, manifest and supplement files for **Dad Studio** aKA **Daz Studio** content.

## Important Notice!

This utility **does not** generates the **metadata** for your **content**. You need to create metadata using **Content Editor** supplied in **Daz Studio**.

[Watch video by cortezinfo on how to create metadata using Content Editor in Daz Studio.](https://www.youtube.com/watch?v=S3_FhmD47pw)
## Required Parameters for DIM Package Name.
**Source Prefix**
>[A sequence of capital letters in the English alphabet and/or numbers, used to identify the source of the file. The “IM”, “DZ”, “DAZ” and “DAZ3D” prefixes are reserved for use by \[DAZ 3D\]. Other prefixes can be used to identify products provided by other sources. Prefixes used to represent other sources must start with at least one letter and can optionally be followed by up to six letters and/or numbers, for a total of seven characters; e.g. A-Z, 0-9; lowercase letters and special characters are not supported.](http://docs.daz3d.com/doku.php/public/software/install_manager/referenceguide/tech_articles/package_naming/start)

**Product SKU**
>[A zero padded eight (8) digit integer value that represents the Stock Keeping Unit \"sku\" used to uniquely identify the product. This value can be shared between multiple packages belonging to the same product. Product SKUs/IDs must be unique..](http://docs.daz3d.com/doku.php/public/software/install_manager/referenceguide/tech_articles/package_naming/start)

**Package ID**
>[A zero padded two (2) digit integer value used to uniquely identify a downloadable file provided by the product. This value should not be used more than once within the same product. Once this value is associated with the type of contents in the package, it should not be changed.](http://docs.daz3d.com/doku.php/public/software/install_manager/referenceguide/tech_articles/package_naming/start)

**Product Name**

>You can find it **Daz Studio** when you open **edit metadata** dialog for your product.
## Required Parameters for Manifest & Supplement files.
**Content Directory Path**
>Path of your product content directory.

**Global ID**

>You can find it **Daz Studio** when you open **edit metadata** dialog for your product.

**Product Name**

>You can find it **Daz Studio** when you open **edit metadata** dialog for your product.
## Batch Processing Via Text File

You can generate manifest & supplement files for multiple product using a simple text file. Each value should be seperated by **""** double quote marks. 
Make sure text file is formatted as shown below.
### Text File Format
>***Path to content directory***""***Global ID***""***Product Name*** 

**Example: Single Product**
>C:/WhateverDirectory/Product/Content""Whatever-GlobalID-S""Whatever Product

**Example: Two Different Products**
>C:/WhateverDirectory/ProductA/Content""Whatever-GlobalID-A""Whatever Product A
>C:/WhateverDirectory/ProductB/Content""Whatever-GlobalID-B""Whatever Product B

*and so on...*

## Batch Processing Normal

You can generate manifest & supplement files for multiple products one at a time. Application will ask for **Content Directory** & **Global ID** and **Product Name** for each cycle. 
>Cycle will continue as long as you keep typing **1** in console after each completion. 
>You can quit the program after each completion of cycle by typing **0** in console.

## Upcoming Improvements

 - Built in achiever for your content.

- Reduce manual data entry through parsing product metadata file.
- Cross platform GUI
##
Copyright (c) 2021 Mohammad Awais
