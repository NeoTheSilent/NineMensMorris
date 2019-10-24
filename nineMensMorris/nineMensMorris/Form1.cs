﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace nineMensMorris
{
    public partial class Board : Form {
        String test = "";
        bool phase = true; //When true it is the placement phase, when false it is the movement phase.
        bool turn = true; //When true it is player 1's turn, when false it is player 2's turn.
        int delete = 0;
        int player1_tokens = 9;
        int player2_tokens = 9;
        int[] boardArray = new int[25];
        int[] millArray = new int[25];
        int[][] matrix = new int[][] {
            new int[] { 1, 2, 3},
            new int[] { 1, 10, 22 },
            new int[] { 2, 5, 8},
            new int[] { 3, 15, 24 },
            new int[] { 4, 5, 6 },
            new int[] { 4, 11, 19 },
            new int[] { 6, 14, 21 },
            new int[] { 7, 8, 9 },
            new int[] { 7, 12, 16 },
            new int[] { 9, 13, 18 },
            new int[] { 10, 11, 12 },
            new int[] { 13, 14, 15 },
            new int[] { 16, 17, 18 },
            new int[] { 17, 20, 23 },
            new int[] { 19, 20, 21},
            new int[] { 22, 23, 24 }
    };


        public Board() {
            // Initialize boardArray to be full of 0's, meaning those pieces are empty
            for (int i = 1; i < 25; i++)
                boardArray[i] = 0;

            InitializeComponent();
            textBox15.AppendText(Environment.NewLine);
            textBox15.AppendText("Player 1's Turn");
        }
        private void rulesToolStripMenuItem_Click(object sender, EventArgs e) {
            MessageBox.Show("By: Mark Ekis\nBrian Roden\nSungho Lee\nRaymond Rennock", "Creators");
        }

        private void NewGameToolStripMenuItem_Click(object sender, EventArgs e) {
            // Loop through all controls, and re-enable any button that isn't enabled
            // Set each button to its neutral color
            foreach (Control s in ActiveForm.Controls) {
                Button b = s as Button;

                if (b != null) {
                    b.Enabled = true;
                    b.BackColor = System.Drawing.Color.Khaki;
                }
            }

            // Reset boardArray
            for (int i = 0; i < 24; i++)
                boardArray[i] = 0;

            // Reset game variables
            player1_tokens = 9;
            player2_tokens = 9;
            phase = true;
            turn = true;
            delete = 0;

            // Reset the counters for each player's token
            p1Tokens.Text = player1_tokens.ToString();
            p2Tokens.Text = player2_tokens.ToString();

            // Reset the game dialogue box, then inform user the game is reset
            textBox15.ResetText();
            textBox15.AppendText("Phase 1, Placement");
            textBox15.AppendText(Environment.NewLine);
            textBox15.AppendText("Player 1's Turn");
            MessageBox.Show("The game has been reset.");
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void millPlayerOne(int head) {
            for (int i = 0; i < matrix.Length; i++) {
                int a = matrix[i][0];
                int b = matrix[i][1];
                int c = matrix[i][2];

                if (a == head || b == head || c == head) {
                    if (boardArray[a] == 1 && boardArray[b] == 1 && boardArray[c] == 1) {
                        if (millArray[a] != 1 || millArray[b] != 1 || millArray[c] != 1) {
                            millArray[a] = 1;
                            millArray[b] = 1;
                            millArray[c] = 1;
                            delete = 1;
                            textBox15.AppendText(Environment.NewLine);
                            textBox15.AppendText("Player 1 has formed a mill. Select a blue piece to remove.");
                        }
                    }
                }
            }
        }

        private void millPlayerTwo(int head) {
            for (int i = 0; i < matrix.Length; i++) {
                int a = matrix[i][0];
                int b = matrix[i][1];
                int c = matrix[i][2];

                if (a == head || b == head || c == head) {
                    if (boardArray[a] == 2 && boardArray[b] == 2 && boardArray[c] == 2) {
                        if (millArray[a] != 2 || millArray[b] != 2 || millArray[c] != 2) {
                            millArray[a] = 2;
                            millArray[b] = 2;
                            millArray[c] = 2;
                            delete = 2;
                            textBox15.AppendText(Environment.NewLine);
                            textBox15.AppendText("Player 2 has formed a mill. Select an orange piece to remove.");
                        }
                    }
                }
            }
        }
        private void millDetection() {
            for (int i = 1; i < 25; i++) {
                if (boardArray[i] == 1)
                    millPlayerOne(i);
            }

            for (int i = 1; i < 25; i++) {
                if (boardArray[i] == 2)
                    millPlayerTwo(i);
            }

            test = "";
            for (int i = 1; i < millArray.Length; i++)
                test += Convert.ToString(millArray[i]) + ", ";

            test += "\n";

            for (int i = 1; i < boardArray.Length; i++)
                test += Convert.ToString(boardArray[i] + ", ");
        }

        private void delete_piece(Button deleteButton, int head) {
            deleteButton.BackColor = Color.Khaki;
            deleteButton.Enabled = true;
            boardArray[deleteButton.TabIndex] = 0;

            for (int i = 0; i < matrix.Length; i++) {
                int a = matrix[i][0];
                int b = matrix[i][1];
                int c = matrix[i][2];

                if (a == head || b == head || c == head) {
                    millArray[a] = 0;
                    millArray[b] = 0;
                    millArray[c] = 0;
                }

            }

            millDetection();
        }

        private void button_click(object sender, EventArgs e) { 
            while (delete == 0) {
                // If player 1 selects an empty piece, then that index in boardArray becomes 1
                // If player 2 selects an empty piece, then that index in boardArray becomes 2
                if (phase == true && player2_tokens > 0) {
                    Button b = (Button)sender;
                    if (turn) {
                        b.BackColor = Color.OrangeRed;
                        boardArray[b.TabIndex] = 1;
                        player1_tokens--;
                        millDetection();
                        p1Tokens.Text = player1_tokens.ToString();
                        if (delete == 0) {
                            textBox15.AppendText(Environment.NewLine);
                            textBox15.AppendText("Player 2's Turn");
                        }

                    }
                    else {
                        b.BackColor = Color.Aqua;
                        boardArray[b.TabIndex] = 2;
                        player2_tokens--;
                        millDetection();
                        p2Tokens.Text = player2_tokens.ToString();
                        if (delete == 0) {
                            textBox15.AppendText(Environment.NewLine);
                            textBox15.AppendText("Player 1's Turn");
                        }
                    }
                    turn = !turn;
                    b.Enabled = false;
                    break;
                }
                if (phase == true && player1_tokens <= 0 && player2_tokens <= 0) {
                    phase = false;
                    textBox15.AppendText(Environment.NewLine);
                    textBox15.AppendText("Begining Phase 2, Movement");
                }

            }

            if (delete == 1) {
                Button buttonToDelete = (Button)sender;
                bool onlyMill = true;

                foreach (Control s in ActiveForm.Controls) {
                    Button b = s as Button;

                    if (b != null) {
                        if (!b.Enabled && b.BackColor == Color.Aqua) {
                            b.Enabled = true;
                        }
                    }
                }


                if (buttonToDelete.Enabled) {
                    if (boardArray[buttonToDelete.TabIndex] == 2) {
                        if (millArray[buttonToDelete.TabIndex] == 2) {
                            for (int i = 1; i < boardArray.Length; i++) {
                                if (boardArray[i] != millArray[i])
                                    onlyMill = false;
                            }

                            if (onlyMill == true) {
                                delete_piece(buttonToDelete, buttonToDelete.TabIndex);
                                foreach (Control s in ActiveForm.Controls) {
                                    Button b = s as Button;

                                    if (b != null) {
                                        if (b.Enabled && b.BackColor == Color.Aqua) {
                                            b.Enabled = false;
                                        }
                                    }
                                }
                                delete = 0;
                                textBox15.AppendText(Environment.NewLine);
                                textBox15.AppendText("Player 2's turn");
                            }

                        }

                        else {
                            delete_piece(buttonToDelete, buttonToDelete.TabIndex);
                            foreach (Control s in ActiveForm.Controls) {
                                Button b = s as Button;

                                if (b != null) {
                                    if (b.Enabled && b.BackColor == Color.Aqua) {
                                        b.Enabled = false;                            
                                    }
                                }
                            }
                            delete = 0;
                            textBox15.AppendText(Environment.NewLine);
                            textBox15.AppendText("Player 2's turn");
                        }
                    }
                } 
            }

            if (delete == 2) {
                Button buttonToDelete = (Button)sender;
                bool onlyMill = true;

                foreach (Control s in ActiveForm.Controls) {
                    Button b = s as Button;

                    if (b != null) {
                        if (!b.Enabled && b.BackColor == Color.OrangeRed) {
                            b.Enabled = true;
                        }
                    }
                }


                if (buttonToDelete.Enabled) {
                    if (boardArray[buttonToDelete.TabIndex] == 1) {
                        if (millArray[buttonToDelete.TabIndex] == 1) {
                            for (int i = 1; i < boardArray.Length; i++) {
                                if (boardArray[i] != millArray[i])
                                    onlyMill = false;
                            }

                            if (onlyMill == true) {
                                delete_piece(buttonToDelete, buttonToDelete.TabIndex);
                                foreach (Control s in ActiveForm.Controls) {
                                    Button b = s as Button;

                                    if (b != null) {
                                        if (b.Enabled && b.BackColor == Color.OrangeRed) {
                                            b.Enabled = false;
                                        }
                                    }
                                }
                                delete = 0;
                                textBox15.AppendText(Environment.NewLine);
                                textBox15.AppendText("Player 1's turn");
                            }

                        }

                        else {
                            delete_piece(buttonToDelete, buttonToDelete.TabIndex);
                            foreach (Control s in ActiveForm.Controls) {
                                Button b = s as Button;

                                if (b != null) {
                                    if (b.Enabled && b.BackColor == Color.OrangeRed) {
                                        b.Enabled = false;
                                    }
                                }
                            }
                            delete = 0;
                            textBox15.AppendText(Environment.NewLine);
                            textBox15.AppendText("Player 1's turn");
                        }
                    }
                }
            }
        }

        private void displayMillsToolStripMenuItem_Click(object sender, EventArgs e) {
            MessageBox.Show(test);
        }
    }  
}