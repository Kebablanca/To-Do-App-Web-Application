<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="AddEvent.aspx.cs" Inherits="halit.AddEvent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Event</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 20px;
        }

        form {
            max-width: 400px;
            margin: 0 auto;
            background-color: #fff;
            padding: 20px;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        label {
            display: block;
            margin-bottom: 8px;
        }

        input {
            width: 100%;
            padding: 8px;
            margin-bottom: 15px;
            box-sizing: border-box;
        }

        button {
            background-color: #4caf50;
            color: #fff;
            padding: 10px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }
    </style>
    <script>
        function showErrorPopup() {
            alert('Failed to save data. Please try again.');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label for="txtDate">Date:</label>
            <asp:TextBox ID="txtDate" runat="server" type="date" CssClass="form-control"></asp:TextBox>
            <br />
            <label for="txtTitle">Title:</label>
            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"></asp:TextBox>
            <br />
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary" />
        </div>
    </form>
</body>
</html>
