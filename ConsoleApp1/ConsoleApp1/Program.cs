using System;


namespace Napilnik1
{
    class Program
    {
        static void Main(string[] args)
        {
            private void OnClickButton(object sender, EventArgs e)
            {
                if (this.passportTextbox.Text.Trim() == "")
                    int messagesContainer1 = (int)MessageBox.Show("Введите серию и номер паспорта");
                else
                    VerifyPasportInformation();
            }

            private void VerifyPasportInformation()
            {
                int correctInformationLength = 10;

                string rawData = this.passportTextbox.Text.Trim().Replace(" ", string.Empty);

                if (rawData.Length < correctInformationLength)
                    this.textResult.Text = "Неверный формат серии или номера паспорта";
                else
                {
                    string commandText = string.Format("select * from passports where num='{0}' limit 1;", (object)Form1.ComputeSha256Hash(rawData));
                    string connectionString = string.Format("Data Source=" + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\db.sqlite");

                    FindRequiredFile();
                }
            }

            private void FindRequiredFile()
            {
                int errorCodeNumber = 1;

                try
                {
                    SQLiteConnection connection = new SQLiteConnection(connectionString);
                    connection.Open();

                    SQLiteDataAdapter sqLiteDataAdapter = new SQLiteDataAdapter(new SQLiteCommand(commandText, connection));
                    DataTable dataTable1 = new DataTable();
                    DataTable dataTable2 = dataTable1;
                    sqLiteDataAdapter.Fill(dataTable2);

                    FindPassport(dataTable1, connection);
                }
                catch (SQLiteException ex)
                {
                    if (ex.ErrorCode != errorCodeNumber)
                        return;
                    int messagesContainer2 = (int)MessageBox.Show("Файл db.sqlite не найден. Положите файл в папку вместе с exe.");
                }
            }

            private void FindPassport(DataTable dataTable1, SQLiteConnection connection)
            {
                int firstRawIndex = 0;
                int secondItemIndex = 1;

                if (dataTable1.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dataTable1.Rows[firstRawIndex].ItemArray[secondItemIndex]))
                        this.textResult.Text = "По паспорту «" + this.passportTextbox.Text + "» доступ к бюллетеню на дистанционном электронном голосовании ПРЕДОСТАВЛЕН";
                    else
                        this.textResult.Text = "По паспорту «" + this.passportTextbox.Text + "» доступ к бюллетеню на дистанционном электронном голосовании НЕ ПРЕДОСТАВЛЯЛСЯ";
                }
                else
                    this.textResult.Text = "Паспорт «" + this.passportTextbox.Text + "» в списке участников дистанционного голосования НЕ НАЙДЕН";
                connection.Close();
            }
        }
    }
}
    






