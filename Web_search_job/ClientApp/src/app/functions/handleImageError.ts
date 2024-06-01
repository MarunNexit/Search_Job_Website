export function handleImageError(event: Event | undefined, imageUrl: string) {
  if (event) {
    const target = event.target as HTMLImageElement;
    if (target.src !== imageUrl) {
      target.src = imageUrl;
    }
  }
}
