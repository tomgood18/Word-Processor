using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment2
{
    public partial class Form3 : Form
    {
        public List<User> list { get; set; }

        public Form3(List<User> userList)
        {
            InitializeComponent();
            // Initializes 'type box' from enum defined at the bottom
            userTypeBox.DataSource = Enum.GetValues(typeof(userType));
            dateOfBirthPicker.CustomFormat = "dd-MM-yyyy";
            this.Text = "New User";
            list = userList;
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            // If username is available and passwords match, create user
            if (checkFields(true))
            {
                createUser();
                MessageBox.Show("User successfully created", "Success", MessageBoxButtons.OK, MessageBoxIcon.None);
                Form1 firstForm = new Form1(list);
                this.Hide();
                firstForm.ShowDialog();
                this.Close();
            } else 
            {
                MessageBox.Show("Make sure your username is unique, passwords match, password is 4 characters or more", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Form1 firstForm = new Form1(list);
            this.Hide();
            firstForm.ShowDialog();
            this.Close();
        }

        private bool checkFields(bool authenticated)
        {
            // Read from user list to check for duplicates, password match, password length
            int x = 0;
            while (x < list.Count())
            {
                String tempUser = list[x].username;
                if (usernameText.Text.Equals(tempUser))
                {
                    authenticated = false;
                    return authenticated;
                } 
                x++;
            }
            if (!passwordText.Text.Equals(passwordConfirmText.Text))
            {
                authenticated = false;
                return authenticated;
            } if (passwordText.TextLength < 4)
            {
                authenticated = false;
                return authenticated;
            }
            authenticated = true;
            return authenticated;
        }

        private void createUser()
        {
            // Creates new user and appends to user list
            User user = new User(usernameText.Text, passwordText.Text, firstNameText.Text, lastNameText.Text, userTypeBox.SelectedItem.ToString(), dateOfBirthPicker.Text);
            list.Add(user);
        }
    }

    public enum userType
        // Enum for user type
    {
        View,Edit
    }
}