#  Turbolinks in ASP.NET MVC
Demonstrates the [Rails TurboLinks](https://github.com/rails/turbolinks/) 
integration in ASP.NET MVC. Please note that all the rules/tricks/gotchas of 
the original Turbolinks also applies here.

The sample originated from [RailsCasts 390 episode](http://railscasts.com/episodes/390-turbolinks) 
but also utilizes the built-in Ajax helpers of ASP.NET MVC for seamless smooth
ux.

[Install via nuget](https://www.nuget.org/packages/aspnetmvcturbolinks/).

### Performance metrics:

```
Results for 10 clicks:
====================================
Without Turbolinks: 00:00:04.7515689
   With Turbolinks: 00:00:02.7349811
        Difference: 00:00:02.0165878

Results for 25 clicks:
====================================
Without Turbolinks: 00:00:11.7801921
   With Turbolinks: 00:00:07.3560813
        Difference: 00:00:04.4241108

Results for 50 clicks:
====================================
Without Turbolinks: 00:00:23.3691119
   With Turbolinks: 00:00:13.2038149
        Difference: 00:00:10.1652970

Results for 100 clicks:
====================================
Without Turbolinks: 00:00:46.4933315
   With Turbolinks: 00:00:26.5189392
        Difference: 00:00:19.9743923

Results for 250 clicks:
====================================
Without Turbolinks: 00:01:57.9627894
   With Turbolinks: 00:01:07.2411878
        Difference: 00:00:50.7216016
```
