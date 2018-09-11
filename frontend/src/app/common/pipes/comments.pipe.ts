import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'comments'
})
export class CommentsPipe implements PipeTransform {

  transform(value: string, mode: string): any {
    let result;
    if (mode === 'selectedText') {
      result = value.match(/-->(.*?)<--/g).toString();
      result = result.substring(3, result.length - 3);
      result = `\"${result}\": `;
    } else if (mode === 'commentText') {
      let selectedText = value.match(/-->(.*?)<--/g);
      if (!selectedText) {
        result = value;
      } else {
        result = value.replace(selectedText.toString(), '');
      }
    }
    return result;
  }

}
