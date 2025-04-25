# Cabbage Panel
An open source software application for browsing a pterodactyl panel.

![cabbage-panel](https://github.com/user-attachments/assets/0efa2098-2cdf-41d2-b1e2-987284e4fdf4)

## About
Cabbage Panel allows you to browse and explore any Pterodactyl host through a standalone Windows application. Cabbage Panel is written in C#/.NET and utilizes the Windows Presentation Foundation (WPF) framework. The modern Windows aesthetic is achieved through [WPF UI](https://wpfui.lepo.co/). Access to the host is acquired through the vanilla Pterodactyl API, and simply requires you to enter a client API key to log in.

## How to set a custom host
To easily customize the URL that the application points to, simply modify the `customUrl.panel` file contained within the same directory as the executable. Ensure that this URL ends with a "/" or you will get an error when attempting to run the application if it does not. Once you run the program after having set this value, your application will now point to the target host.
![image](https://github.com/user-attachments/assets/2d63cfbc-2186-4511-8206-811b5c261042)

## Issues and requests
If you find any bugs with the app or if you would like to request additional features, I would recommend opening a thread in the "Issues" tab of this Github page. I am happy to take any suggestions.

## Why does this exist?
Fun. I just wanted to mess around with the Pterodactyl API and I am a .NET developer. I did not design this with any real practical application in mind, as web apps are way more convenient for consumers anyway.
