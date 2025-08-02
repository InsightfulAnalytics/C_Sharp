
// Set IsAvailableInMDX to FALSE for ALL columns

    // https://data-mozart.com/hidden-little-gem-that-can-save-your-power-bi-life/

    foreach(var column in Model.AllColumns)
        column.IsAvailableInMDX = false;



// 