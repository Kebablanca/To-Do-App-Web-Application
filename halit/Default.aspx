<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="halit.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Calendar Page</title>
    <style>
        body {
            background-image: url('https://img.freepik.com/premium-photo/blurred-water-background-reflecting-green-nature_49758-849.jpg');
            background-repeat: no-repeat;
            background-size: cover;
            font-family: Arial, sans-serif;
            background-color: #f2f2f2;
            margin: 0;
            padding: 0;
        }

        .center-button {
            text-align: center;
        }

        .container {
            max-width: 800px;
            margin: 20px auto;
            background-color: #e9f2e5;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 0 30px #666;
        }

        h2 {
            color: #333;
        }

        .btn {
            background-color: #4CAF50;
            color: white;
            padding: 10px 15px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }

            .btn:hover {
                background-color: #45a049;
            }

        #calDisplay {
            width: 100%;
            margin: 20px 0;
            border-radius: 12px;
            overflow: hidden; 
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.2); 
        }

            #calDisplay tr {
                height: 50px; 
            }

            #calDisplay td {
                text-align: center;
                padding: 15px; 
                cursor: pointer;
                border-radius: 8px; 
                transition: background-color 0.3s ease; 

            }

                #calDisplay td:hover {
                    background-color: #e0e0e0;
                }

        .selected-day {
            text-decoration: none !important;
            background-color: #99ccff;
            border-radius: 50%;
            color: #fff !important;
        }

        .events-container {
            margin-top: 20px;
        }

        .selected-date {
            font-size: 18px;
            color: #333;
            font-weight: bold;
        }

        .event-list {
            list-style: none;
            padding: 0;
        }

        .event-list-item {
            margin-bottom: 10px;
            padding: 10px;
            background-color: #e0e0e0;
            border-radius: 4px;
        }

            .event-list-item h3 {
                margin: 0;
                color: #333;
            }

            .event-list-item p {
                margin: 5px 0;
                color: #666;
            }

        .highlighted-day,
        .events-day {
            text-decoration: none !important;
            background-color: #FF7F00;
            padding: 8px;
            border-radius: 50%;
            color: #fff !important;
            font-size: 0.8em;
        }

        .Day {
            text-decoration: none !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Calendar Page</h2>
            <asp:Calendar ID="calDisplay" runat="server" OnSelectionChanged="calDisplay_SelectionChanged" OnDayRender="calDisplay_DayRender" CellSpacing="4"
                DayStyle-Height="70px" Font-Underline="false" DayStyle-Font-Underline="false" DayStyle-BorderWidth="2px" DayStyle-Font-Size="35px"
                NextMonthText="Next Month" PrevMonthText="Previous Month"
                OtherMonthDayStyle-ForeColor="Gray" OtherMonthDayStyle-Font-Size="0"
                OtherMonthDayStyle-Height="0" OtherMonthDayStyle-BorderWidth="0" OtherMonthDayStyle-Width="0" OtherMonthDayStyle-BackColor="Transparent"></asp:Calendar>
            <div class="container center-button">
                <asp:Button ID="btnAddEvent" runat="server" Text="Add Event" OnClick="btnAddEvent_Click" CssClass="btn btn-primary" />
            </div>

            <div>

                <asp:Label ID="lblSelectedDate" runat="server" Text="Selected Date: "></asp:Label>
                <asp:Literal ID="litSelectedDate" runat="server"></asp:Literal>
            </div>

            <div>
                <h3>Events on Selected Date</h3>
                <asp:Literal ID="litEventDates" runat="server"></asp:Literal>
            </div>
        </div>
    </form>
</body>
</html>
