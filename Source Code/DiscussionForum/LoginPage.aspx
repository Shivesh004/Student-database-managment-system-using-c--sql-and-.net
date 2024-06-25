<%@ Page Title="Login Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="DiscussionForum.LoginPage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div runat="server" class="flex justify-center mt-8 relative gap-5">
    <!-- Login Form -->
    <div class="max-w-md w-full bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4">
        <h2 class="text-lg font-bold mb-4">Login</h2>
        <span id="loginError" runat="server"></span>
        <div class="mb-4">
            <asp:Label ID="lblEmail" runat="server" CssClass="block text-gray-700 text-sm font-bold mb-2" AssociatedControlID="txtLoginEmail">Email</asp:Label>
            <asp:TextBox ID="txtLoginEmail" runat="server" CssClass="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" TextMode="Email" placeholder="Email"></asp:TextBox>
        </div>
        <div class="mb-4">
            <asp:Label ID="lblPassword" runat="server" CssClass="block text-gray-700 text-sm font-bold mb-2" AssociatedControlID="txtLoginPassword">Password</asp:Label>
            <asp:TextBox ID="txtLoginPassword" runat="server" CssClass="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 mb-3 leading-tight focus:outline-none focus:shadow-outline" TextMode="Password" placeholder="Password"></asp:TextBox>
        </div>
        <div class="flex items-center justify-between">
            <asp:Button ID="btnLogin" runat="server" CssClass="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline" Text="Login" OnClick="HandleLogin"  />
           
        </div>
    </div>

    
        <span class="absolute top-[200px] font-semibold">
            OR
        </span>
   

    <!-- Registration Form -->
    <div class="max-w-md w-full bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4 ml-4">
        <h2 class="text-lg font-bold mb-4">Register</h2>
        <div id="registerError" runat="server"></div>
     <div class="mb-4">
         <asp:Label ID="lblRegisterUsername" runat="server" CssClass="block text-gray-700 text-sm font-bold mb-2" AssociatedControlID="txtRegisterUsername">Username</asp:Label>
         <asp:TextBox ID="txtRegisterUsername" runat="server" CssClass="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" TextMode="SingleLine" placeholder="Username"></asp:TextBox>
     </div>
        <div class="mb-4">
            <asp:Label ID="lblRegisterEmail" runat="server" CssClass="block text-gray-700 text-sm font-bold mb-2" AssociatedControlID="txtRegisterEmail">Email</asp:Label>
            <asp:TextBox ID="txtRegisterEmail" runat="server" CssClass="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" TextMode="Email" placeholder="Email"></asp:TextBox>
        </div>
        <div class="mb-4">
            <asp:Label ID="lblRegisterPassword" runat="server" CssClass="block text-gray-700 text-sm font-bold mb-2" AssociatedControlID="txtRegisterPassword">Password</asp:Label>
            <asp:TextBox ID="txtRegisterPassword" runat="server" CssClass="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 mb-3 leading-tight focus:outline-none focus:shadow-outline" TextMode="Password" placeholder="Password"></asp:TextBox>
        </div>
        <div class="mb-4">
            <asp:Label ID="lblConfirmPassword" runat="server" CssClass="block text-gray-700 text-sm font-bold mb-2" AssociatedControlID="txtConfirmPassword">Confirm Password</asp:Label>
            <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 mb-3 leading-tight focus:outline-none focus:shadow-outline" TextMode="Password" placeholder="Confirm Password"></asp:TextBox>
        </div>
        <div class="flex items-center justify-between">
            <asp:Button ID="btnRegister" runat="server" CssClass="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline" Text="Register" OnClick="HandleRegister" />
        </div>
    </div>
</div>

</asp:Content>
