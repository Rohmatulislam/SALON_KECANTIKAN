Imports MySql.Data.MySqlClient
Public Class FormUser
    Sub RelodData()
        TxtCode.Text = ""
        TxtNama.Text = ""
        TxtPass.Text = ""
        CmbStatus.Text = ""
        BtnTambah.Enabled = True
        BtnTambah.Text = "Tambah"
        BtnEdit.Enabled = True
        BtnEdit.Text = "Edit"
        BtnHapus.Enabled = True
        BtnHapus.Text = "Hapus"
        BtnTutup.Enabled = True
        BtnTutup.Text = "Tutup"
        CmbStatus.Items.Clear()
        CmbStatus.Items.Add("AKTIF")
        CmbStatus.Items.Add("TIDAK AKTIF")
        Call OpenGrid()
        TxtPass.PasswordChar = "*"
        TxtCode.Enabled = False
        TxtNama.Enabled = False
        TxtPass.Enabled = False
        CmbStatus.Enabled = False
    End Sub
    Sub Hidup()
        TxtCode.Enabled = True
        TxtNama.Enabled = True
        TxtPass.Enabled = True
        CmbStatus.Enabled = True
    End Sub
    Private Sub FormUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call RelodData()
        DataGridView1.DataSource = (Ds.Tables("tbl_user"))
        DataGridView1.Columns(1).Width = 180
        DataGridView1.Columns(2).Width = 120
    End Sub
    Sub OpenGrid()
        Call OpenConn()
        Da = New MySqlDataAdapter("select * from tbl_user", Conn)
        Ds = New DataSet
        Da.Fill(Ds, "tbl_user")
        DataGridView1.DataSource = Ds.Tables("tbl_user")
        DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Silver
    End Sub

    Private Sub Btntambah_Click(sender As Object, e As EventArgs) Handles BtnTambah.Click
        If BtnTambah.Text = "Tambah" Then
            BtnTambah.Text = "Simpan"
            BtnEdit.Enabled = False
            BtnHapus.Enabled = False
            BtnTutup.Text = "&Cancel"
            Call Hidup()
            TxtCode.Focus()
        Else
            If TxtCode.Text = "" Or TxtNama.Text = "" Or TxtPass.Text = "" Or CmbStatus.Text = "" Then
                MsgBox("Pastikan Semua Data Terisi !!!")
                Call Hidup()
                TxtCode.Focus()
            Else
                Call OpenConn()
                Dim InputData As String = "insert into tbl_user (Kode_User, Nama_User, Password_User, Status_User) values ('" & TxtCode.Text & "','" & TxtNama.Text & "','" & TxtPass.Text & "','" & CmbStatus.Text & "')"
                Cmd = New MySqlCommand(InputData, Conn)
                Cmd.ExecuteNonQuery()
                MsgBox("Input Data Berhasil !")
                Call RelodData()
            End If
        End If
    End Sub

    Private Sub BtnEdit_Click(sender As Object, e As EventArgs) Handles BtnEdit.Click
        If BtnEdit.Text = "Edit" Then
            BtnEdit.Text = "Update"
            BtnTambah.Enabled = False
            BtnHapus.Enabled = False
            BtnTutup.Text = "&Cancel"
            Call Hidup()
            TxtCode.Focus()
        Else
            If TxtCode.Text = "" Or TxtNama.Text = "" Or TxtPass.Text = "" Or CmbStatus.Text = "" Then
                MsgBox("Pastikan Semua Data Terisi !!!")
                Call Hidup()
                TxtCode.Focus()
            Else
                Call OpenConn()
                Dim Edit As String = "Update tbl_user set Nama_User='" & TxtNama.Text & "',Password_User  = '" & TxtPass.Text & "',Status_User = '" & CmbStatus.Text & "'where Kode_User ='" & TxtCode.Text & "'"
                Cmd = New MySqlCommand(Edit, Conn)
                Cmd.ExecuteNonQuery()
                MsgBox("EDIT DATA BERHASIL !")
                Call RelodData()
            End If
        End If
    End Sub

    Private Sub BtnHapus_Click(sender As Object, e As EventArgs) Handles BtnHapus.Click
        If BtnHapus.Text = "Hapus" Then
            BtnHapus.Text = "Delete"
            BtnTambah.Enabled = False
            BtnEdit.Enabled = False
            BtnTutup.Text = "&Cancel"
            Call Hidup()
            TxtCode.Focus()
        Else
            If TxtCode.Text = "" Or TxtNama.Text = "" Or TxtPass.Text = "" Or CmbStatus.Text = "" Then
                MsgBox("Pastikan Semua Data Terisi !!!")
                Call Hidup()
                TxtCode.Focus()
            Else
                Call OpenConn()
                Dim Hapus As String = "Delete From tbl_user Where Kode_User='" & TxtCode.Text & "'"
                Cmd = New MySqlCommand(Hapus, Conn)
                Cmd.ExecuteNonQuery()
                MsgBox("DATA BERHASIL DI HAPUS", MsgBoxStyle.Information, "INFORMATION")
                Call RelodData()
            End If
        End If
    End Sub
    Private Sub TxtCode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtCode.KeyPress
        If e.KeyChar = Chr(13) Then
            Call OpenConn()
            Cmd = New MySqlCommand("Select * from tbl_user where Kode_User='" & TxtCode.Text & "'", Conn)
            Rd = Cmd.ExecuteReader
            Rd.Read()
            If Not Rd.HasRows Then
                MsgBox("Data Tidak Ada")
            Else
                TxtCode.Text = Rd.Item("Kode_User")
                TxtNama.Text = Rd.Item("Nama_User")
                TxtPass.Text = Rd.Item("Password_User")
                CmbStatus.Text = Rd.Item("Status_User")
            End If
        End If
    End Sub

    Private Sub TxtCode_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtCode.KeyDown
        If e.KeyCode = Keys.Enter Then
            TxtNama.Focus()
        End If
    End Sub
    Private Sub TxtNama_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtNama.KeyPress
        If e.KeyChar = Chr(13) Then
            TxtPass.Focus()
        End If
    End Sub
    Private Sub TxtPass_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPass.KeyPress
        If e.KeyChar = Chr(13) Then
            CmbStatus.Focus()
        End If
    End Sub
    Private Sub CmbStatus_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbStatus.KeyPress
        If e.KeyChar = Chr(13) Then
            BtnTambah.Focus()
            BtnEdit.Focus()
            BtnHapus.Focus()
        End If
    End Sub

    Private Sub BtnTutup_Click(sender As Object, e As EventArgs) Handles BtnTutup.Click
        If BtnTutup.Text = "Tutup" Then
            Me.Close()
        Else
            Call RelodData()
        End If
    End Sub
End Class