using System;
using System.Collections;
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
    public partial class Form1 : Form
    {
        public String username;
        public String userType;
        String password;
        public List<User> list;

        public Form1(List<User> userList)
        {
            InitializeComponent();
            this.Text = "Login";
            list = userList;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // If the user list is empty, load them into a list from the login.txt file
            if (list.Count == 0)
            {
                loadUsers();
            }
            // If the users had already been read from the login.txt file, rewrite to the login.txt file with the existing list.
            else
            {
                int x = 0;
                File.WriteAllText(@"login.txt", String.Empty);
                while (x < list.Count())
                {
                    using (StreamWriter file = new StreamWriter(@"login.txt", true))
                    {
                        file.WriteLine(list[x].username + "," + list[x].password + "," + list[x].userType + "," + list[x].firstName + "," + list[x].lastName + "," + list[x].dateOfBirth);
                    }
                    x++;
                }
            }
        }

        public void loadUsers()
        {
            int counter = 0;
            string line;
            // Read the login.txt file and append to a user list 
            StreamReader file = new StreamReader(@"login.txt");
            while ((line = file.ReadLine()) != null)
            {
                var listSplit = line.Split(',');
                User user = new User(listSplit[0], listSplit[1], listSplit[3], listSplit[4], listSplit[2], listSplit[5]);
                list.Add(user);
                counter++;
            }
            file.Close();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            Form2 secondForm = new Form2(list);
            setUsername();
            setPassword();
            if (readLogin(true))
            {
                secondForm.userLabel = username;
                secondForm.userType = userType;
                this.Hide();
                secondForm.ShowDialog();
                this.Close();           //Allows the application to exit completely when closed.
            }
            else
            {
                MessageBox.Show("Incorrect login details, please try again", "Login failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NewUserButton_Click(object sender, EventArgs e)
        {
            Form3 thirdform = new Form3(list);
            this.Hide();
            thirdform.ShowDialog();
            this.Close();
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private bool readLogin(bool authenticated)
            // Returns authentication boolean
        {
            int x = 0;
            while (x < list.Count())
            {
                String tempUser = list[x].username;
                String tempPass = list[x].password;
                String tempType = list[x].userType;
                if (username.Equals(tempUser))
                {
                    if (password.Equals(tempPass))
                    {
                        username = tempUser;
                        password = tempPass;
                        userType = tempType;
                        authenticated = true;
                        return authenticated;
                    } else
                    {
                        authenticated = false;
                    }
                }
                 else
                {
                    authenticated = false;
                }
                x++;
            }
            return authenticated;
        }

        private void setUsername()
        {
            username = usernameText.Text;
        }

        private void setPassword()
        {
            password = passwordText.Text;
        }
    }

    // Creates user object
    public class User
    {
        public String username;
        public String password;
        public String firstName;
        public String lastName;
        public String userType;
        public String dateOfBirth;

        public User()
        {

        }
        public User(String username, String password, String firstName, String lastName, String userType, String dateOfBirth)
        {
            this.username = username;
            this.password = password;
            this.firstName = firstName;
            this.lastName = lastName;
            this.userType = userType;
            this.dateOfBirth = dateOfBirth;
        }
    }
}
