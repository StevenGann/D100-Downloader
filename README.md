# D100-Downloader
A simple utility to export D100 tables

Features:

- [x] Download lists from dndspeak.com URLs
- [ ] Download lists from reddit.com/r/d100/
- [x] Export list as spreadsheett (TSV)
- [X] Export list as printable HTML
- [ ] Export list as PDF
- [X] Configurable styling

Feature requests, bug reports, and feedback all greatly appreciated!


## Print Styling

When exporting to aprintable format, D100Downloader generates an HTML document from the list data using a template pulled from the template.html file.

This file can be easily customized. During the generation process, D100Downloader looks for a couple important strings:

- `<!--####TITLE####-->`
    - Optional. All occurances of this tag are replaced with the title of the list.
- `<!--####SUBTITLE####-->`
    - Optional. All occurances of this tag are replaced with the subtitle of the list.
- `<!--####START LIST####-->`
    - Required. This tag indicates where the list data should be, and the start of the list item template.
- `<!--####END LIST####-->`
    - Required. This tag indicates the end of the list item template. All text between `<!--####START LIST####-->` and `<!--####END LIST####-->` will be copied for every item in the list, and modified with the data for that list item.
- `<!--####LIST ITEM####-->`
    - Not _technically_ required, but sort of important. Use this within the list item template to mark where the list item's data should go.

### Minimal template example:
```html
<!DOCTYPE html>
<html lang="en">
  <body>
    <!--####START LIST####-->
    <!--####LIST ITEM####--></br>
    <!--####END LIST####-->
  </body>
</html>
```

The above example will simply list all of the list data with no styling or formatting. The default template.html provides a great deal more, including HTML5 compatibility patches, some CSS boilerplate, and structuring the list as an ordered list, i.e. `<ol>`.