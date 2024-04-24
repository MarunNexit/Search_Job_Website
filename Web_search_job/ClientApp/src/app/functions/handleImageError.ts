export function handleImageError(event: Event | undefined, imageUrl: string) {
  if (event) {
    const target = event.target as HTMLImageElement;
    target.src = imageUrl;
  }
}
