# NavigationStudyExampleProject

## 📺 프로젝트 소개 영상
[![ProvincialSkillCompetition](https://img.youtube.com/vi/QX92p2qWuYg/0.jpg)](https://youtu.be/QX92p2qWuYg)

> 위 이미지를 클릭하면 YouTube로 이동합니다.

---

## 📚 프로젝트 개요
- **프로젝트명:** NavigationStudyExampleProject
- **설명:** 이 프로젝트는 Unity로 만든 미로 탐색 시스템입니다.
먼저 MazeManager가 원하는 크기의 타일 지도를 만들고, 각 칸을 길(0) 또는 벽(1)으로 관리합니다.
WallButton을 누르면 특정 칸이 벽으로 바뀌고 색이 검은색으로 표시돼 직관적으로 확인할 수 있습니다.
MazeSolver는 최단 경로 찾기 알고리즘(BFS)을 사용해 출발 지점에서 목표 지점까지 가장 빠른 길을 찾고,
코루틴으로 플레이어를 그 경로를 따라 부드럽게 이동시킵니다.
- **사용 기술 스택:** 
  - Unity
  - C#
