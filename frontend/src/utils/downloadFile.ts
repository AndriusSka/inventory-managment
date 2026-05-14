export function downloadBlob(blob: Blob, fileName: string): void {
  // laikinas URL, kuris nurodo į Blob objektą
  const url = window.URL.createObjectURL(blob);
  // laikinas <a> elementas, kuris bus naudojamas failo atsisiuntimui
  const link = document.createElement("a");
  link.href = url;
  link.download = fileName;
  document.body.appendChild(link);
  link.click();
  // po atsisiuntimo pašalinamas elementas iš DOM ir atlaisvinamas URL
  document.body.removeChild(link);
  window.URL.revokeObjectURL(url);
}
