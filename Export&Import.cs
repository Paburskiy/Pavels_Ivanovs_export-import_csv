private void exportButton_Click(object sender, EventArgs e)
{
    try
    {
        var furnitureList = furnitureManager.ReadFurniture();
        using (var sfd = new SaveFileDialog { Filter = "CSV files (*.csv)|*.csv" })
        {
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (var writer = new StreamWriter(sfd.FileName))
                {
                    writer.WriteLine("Name,Description,Price,Height,Width,Length");
                    foreach (var furniture in furnitureList)
                    {
                        writer.WriteLine($"{furniture.Name},{furniture.Description},{furniture.Price}," +
                                         $"{furniture.Height},{furniture.Width},{furniture.Length}");
                    }
                }
                MessageBox.Show("Eksports bija veiksmīgs.");
            }
        }
    }
    catch (Exception)
    {
        MessageBox.Show("Notikusi kļūda eksportējot datus.");
    }
}

private void importButton_Click(object sender, EventArgs e)
{
    try
    {
        using (var ofd = new OpenFileDialog { Filter = "CSV files (*.csv)|*.csv" })
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                using (var reader = new StreamReader(ofd.FileName))
                {
                    reader.ReadLine(); // Skip header
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split(',');
                        furnitureManager.AddFurniture(parts[0], parts[1],
                            Convert.ToDouble(parts[2]), Convert.ToInt32(parts[3]),
                            Convert.ToInt32(parts[4]), Convert.ToInt32(parts[5]));
                    }
                }
                RefreshComboBox();
                MessageBox.Show("Imports bija veiksmīgs.");
            }
        }
    }
    catch (Exception)
    {
        MessageBox.Show("Notikusi kļūda importējot datus.");
    }
}

private void RefreshComboBox()
{
    var furniture = furnitureManager.ReadFurniture();
    var furnitureNames = furniture.Select(f => f.Name).ToList();
    selectProductComboBox.DataSource = null;
    selectProductComboBox.DataSource = furnitureNames;
}
