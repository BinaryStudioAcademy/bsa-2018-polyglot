import { Injectable } from '@angular/core';
import ImageCompressor from 'image-compressor.js';

@Injectable({
    providedIn: 'root'
})

export class CompressFileService {

    compress(file: any, params: any): Promise<Blob> {
        const ic = new ImageCompressor();
        return ic.compress(file, params);
    }

}
