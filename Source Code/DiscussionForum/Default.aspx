<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DiscussionForum._Default" EnableViewState="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <body class="bg-gray-100">
        <main id="form1" runat="server">
            <div class="container mx-auto py-8">

                <!-- Input form for posting a question -->
                <main class="flex flex-row items-start ">
                    <div class="mb-4 flex-grow w-2/5 flex flex-col mr-5 gap-2">
                        <div>
                            <h1 class="text-3xl font-bold mb-4">Start a Diskussion</h1>
                            <asp:Label ID="lblQuestion" runat="server" AssociatedControlID="txtQuestion" CssClass='block text-gray-700 font-bold mb-2'>Post a Question</asp:Label>
                            <asp:TextBox ID="txtQuestion" runat="server" CssClass="w-full px-3 py-2 border rounded-md"></asp:TextBox>
                            
                            <asp:Button ID="btnPost" runat="server" Text="Post" CssClass="mt-2 px-4 py-2 bg-blue-500 text-white rounded-md hover:bg-blue-600" OnClick="btnPost_Click" />
                        </div>


                        <div class="flex flex-col gap-2">
                            <span class="font-bold text-gray-700">Your DisKussions</span>
                            <% if (Session["UserID"] != null)
                                { %>

                            <div class="bg-gray-200 rounded-xl p-2">
                                <!-- <asp:Literal ID="litUserQuestions" runat="server"></asp:Literal> -->
                                <% foreach (var questionWithReplies in questionsWithRepliesOfUser)
                                    { %>
                                <div class='mb-3 text-lg font-semibold text-gray-600'>
                                    <div class='rounded-t-xl bg-white p-2 pb-0'>
                                        ❓ <%= questionWithReplies.QuestionContent %>
                                    </div>
                                    <div class='rounded-b-xl bg-white pb-2 px-4 text-sm'>
                                        <span>Replies</span>
                                        <div class='replies overflow-y-auto max-h-60'>
                                            <% foreach (var reply in questionWithReplies.Replies)
                                                { %>
                                            <div class='reply ml-2'>
                                                <span class='text-gray-700 font-bold text-sm'>Anonymous</span>
                                                <p><%= reply.ReplyContent %></p>
                                            </div>
                                            <% } %>
                                            <!-- Add more replies if available -->
                                            <div class='mt-2 flex flex-row gap-2'>
                                                <!--
                                                <asp:PlaceHolder ID="ReplyBoxUser" runat="server"></asp:PlaceHolder>
                                                -->
                                                  <asp:TextBox ID="replyContentUser" runat="server" CssClass="w-full px-2 py-1 border rounded-md" />
                                                <asp:Button ID="Button2" runat="server" Text="Reply" CssClass="px-4 py-1 bg-blue-500 text-white rounded-md hover:bg-blue-600" OnClick="btnReplyQ_Click" CommandArgument="<%  questionWithReplies.QuestionId %>" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <% } %>
                            </div>
                            <% } %>
                        </div>

                    </div>

                    <!-- Display area for questions and replies -->
                    <div class="flex-grow w-2/3 flex flex-column gap-3">
                        <span class="font-bold text-2xl">Posted DisKussions</span>
                        <div class="bg-gray-200 rounded-xl p-2">



                            <% foreach (var questionWithReplies in questionsWithReplies)
                                { %>
                            <div class='mb-3 text-lg font-semibold text-gray-600'>
                                <div class='rounded-t-xl bg-white p-2 pb-0'>
                                    ❓ <%= questionWithReplies.QuestionContent %>
                                </div>
                                <div class='rounded-b-xl bg-white pb-2 px-4 text-sm'>
                                    <span>Replies</span>
                                    <div class='replies overflow-y-auto max-h-60'>
                                        <% foreach (var reply in questionWithReplies.Replies)
                                            { %>
                                        <div class='reply ml-2'>
                                            <span class='text-gray-700 font-bold text-sm'>Anonymous</span>
                                            <p><%= reply.ReplyContent %></p>
                                        </div>
                                        <% } %>
                                        <!-- Add more replies if available -->
                                        <div class='mt-2 flex flex-row gap-2'>
                                            <!-- <asp:PlaceHolder ID="ReplyBox" runat="server"></asp:PlaceHolder> -->
                                            <asp:TextBox ID="replyContentQ" runat="server" CssClass="w-full px-2 py-1 border rounded-md" />
                                            <asp:Button ID="Button1" runat="server" Text="Reply" CssClass="px-4 py-1 bg-blue-500 text-white rounded-md hover:bg-blue-600" OnClick="btnReplyQ_Click" CommandArgument="<%= questionWithReplies.QuestionId %>" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <% } %>
                        </div>

                    </div>
                </main>
            </div>
        </main>
    </body>

</asp:Content>
