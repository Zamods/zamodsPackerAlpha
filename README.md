 ![zamodsPacker](https://am3pap003files.storage.live.com/y4mHSveaj4d2ea6dC7RqcOYtgHQS_Yfwxgf8EaFib4SbUWf2j9DPKf6VwLE5-3-UUH0MGBuaWEp6kBq0AmaPA8plZNPqUVBpE2pPtf80ztehn4afqpwIHVQy9f4j3lNyDr4Lps0OR-cYsMt7B4Wsv7w_wORe39G_ICRs-ymo_Bs-5QC6eeA0C2-Npn3vUrIo-jG?width=512&height=256&cropmode=none)
# zamodsPacker
A console utility to generate manifest and supplement files for **Dad Studio** aKA **Daz Studio** content.

## Important Notice!

This utility **does not** generates the **metadata** for your **content**. You need to create metadata using **Content Editor** supplied in **Daz Studio**.

[Watch video by cortezinfo on how to create metadata using Content Editor in Daz Studio.](https://www.youtube.com/watch?v=S3_FhmD47pw)
## Required Parameters
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
