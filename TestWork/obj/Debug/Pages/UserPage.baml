   M S B A M L   `   `   `  ????C  ?TestWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:TestWorkTestWork  5          TestWork.UserPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:TestWork     5        N? Title$  UserPage??5	      .??5
      ?  ? RowDefinitions 5   
   ?? 5      $	??100 ?6   ?? 5      $??* ?6   .+?5   
   ?? -   5        ._?5      ?? $??Log out??6   $??Roboto)?5      $??18(?5      ???#0000CD$??Right=?5      $??	0,20,30,0q?5      5      ?? 5   
   -   5         ??Name$ valCountLabel??5      $??1??5      $	??300??5      $	??Top=?5      $
??Left=?5      $??	30,10,0,0q?5      $??18(?5      $??Roboto)?6"   ??5      ?? *CountChanged+ ?? StringFormat$: 4Количество записей в базе: {0}??5   "   ?? 5   
   -   5         ??Name$ startStopButton??5      
???????
??? ? ?$??30??5"      $??1??5#      $	??Top=?5$      $??Right=?5%      $	??100??6-   $??	0,10,20,0q?5'      $??
Старт??5(       ? 	Resources 5)       ?? 
TargetType ?? Property ?? Value??%^   '??       ?? 5*      " ??6   .?5+      ??  ?@??CornerRadius$ 5??65   5,      5-      5.      5/      50      