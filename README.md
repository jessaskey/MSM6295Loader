# MSM6295Loader
Utility for creating and manipulating OKI MSM6295 ADPCM based samples and ROM images

This is a fairly simple UI program for dealing with OKI specific ADPCM files and binaries, it allows you to.

1. Import an existing ROM image that has 1-127 ADPCM compressed sounds.
2. Play each sample 
3. Add comments/description to each sample
4. Import new WAV or RAW ADPCM data into library
5. Save as a 'project' 
6. Export samples into new MSM6295 ROM image

Notes on the OKI Codec implementation

The codec is specific to the OKI chip and is the Dialogic flavor of ADPCM. I found a number of libraries that 
implmented this but my C# version here is a hybrid/bastard between the MAME decoder and the encoder that is 
in https://github.com/superctr/adpcm. It works but there may be some issues in my implementation/interpretation. 
